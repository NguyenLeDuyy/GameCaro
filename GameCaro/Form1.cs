using System;
using System.Net.NetworkInformation;
using System.Threading;
using System.Windows.Forms;

namespace GameCaro
{
    public partial class Form1 : Form
    {
        #region Properties
        ChessBoardManager ChessBoard;
        SocketManager socket;
        #endregion

        public Form1()
        {
            InitializeComponent();

            Control.CheckForIllegalCrossThreadCalls = false;

            ChessBoard = new ChessBoardManager(pnlChessBoard, txbPlayerName, pctbSign);
            ChessBoard.EndedGame += ChessBoard_EndedGame;
            ChessBoard.PlayerSigned += ChessBoard_PlayerSigned;

            prcbCoolDown.Step = Cons.COOL_DOWN_STEP;
            prcbCoolDown.Maximum = Cons.COOL_DOWN_TIME;
            prcbCoolDown.Value = 0;

            tmCoolDown.Interval = Cons.COOL_DOWN_INTERVAL;

            socket = new SocketManager();

            NewGame();
        }

        #region Methods
        void EndGame()
        {
            tmCoolDown.Stop();
            pnlChessBoard.Enabled = false;
            undoToolStripMenuItem.Enabled = false;
            //MessageBox.Show("Trò chơi kết thúc!");
        }

        void NewGame()
        {
            prcbCoolDown.Value = 0;
            tmCoolDown.Stop();
            undoToolStripMenuItem.Enabled = true;
            ChessBoard.DrawChessBoard();
        }

        void Quit()
        {
            Application.Exit();
        }

        void Undo()
        {
            ChessBoard.Undo();
            prcbCoolDown.Value = 0;
        }

        private void ChessBoard_PlayerSigned(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            pnlChessBoard.Enabled = false;
            prcbCoolDown.Value = 0;
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));

            undoToolStripMenuItem.Enabled = false;

            Listen();
        }

        private void ChessBoard_EndedGame(object? sender, EventArgs e)
        {
            EndGame();

            socket.Send(new SocketData((int)SocketCommand.END_GAME, "", new Point()));

        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                EndGame();
                socket.Send(new SocketData((int)SocketCommand.TIME_OUT, "", new Point()));

            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
            socket.Send(new SocketData((int)SocketCommand.NEW_GAME, "", new Point()));
            pnlChessBoard.Enabled = true;
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
            socket.Send(new SocketData((int)SocketCommand.UNDO, "", new Point()));
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                e.Cancel = true;
            else
            {
                try
                {
                    socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));
                }
                catch { }
            }
        }

        private enum Role { None, Server, Client }
        private Role initialRole = Role.None;

        private void btnLAN_Click(object sender, EventArgs e)
        {
            if (initialRole == Role.None)
            {
                socket.IP = txbIP.Text;

                if (!socket.ConnectServer())
                {
                    socket.isServer = true;
                    pnlChessBoard.Enabled = true;
                    socket.CreateServer();
                    initialRole = Role.Server;
                }
                else
                {
                    MessageBox.Show("Kết nối thành công!");
                    socket.isServer = false;
                    pnlChessBoard.Enabled = false;
                    Listen();
                    initialRole = Role.Client;
                }
            }
            else
            {
                MessageBox.Show("Vai trò ứng dụng đã được áp dụng, không thể thay đổi.");
            }
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            // Kiểm tra kết nối của máy là có dây hay không dây
            txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Wireless80211);

            if (string.IsNullOrEmpty(txbIP.Text))
            {
                txbIP.Text = socket.GetLocalIPv4(NetworkInterfaceType.Ethernet);
            }
        }

        void Listen()
        {
            Thread listenThread = new Thread(() =>
            {
                try
                {
                    SocketData data = (SocketData)socket.Receive(typeof(SocketData));

                    ProcessData(data);
                }
                catch (Exception e)
                {

                }
            });

            listenThread.IsBackground = true;
            listenThread.Start();
        }


        private void ProcessData(SocketData data)
        {
            switch (data.Command)
            {
                case (int)SocketCommand.NOTIFY:
                    MessageBox.Show(data.Message);
                    break;
                case (int)SocketCommand.NEW_GAME:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        NewGame();
                        pnlChessBoard.Enabled = false;
                    }));
                    break;
                case (int)SocketCommand.SEND_POINT:
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pnlChessBoard.Enabled = true;
                        prcbCoolDown.Value = 0;
                        tmCoolDown.Start();
                        ChessBoard.OtherPlayerSign(data.Point);
                        undoToolStripMenuItem.Enabled = true;
                    }));
                    break;
                case (int)SocketCommand.UNDO:
                    Undo();
                    prcbCoolDown.Value = 0;
                    break;
                case (int)SocketCommand.END_GAME:
                    EndGame();
                    MessageBox.Show(this, "Người chơi " + ChessBoard.Player[ChessBoard.Currentplayer == 1 ? 0 : 1].Name + " đã thắng.");
                    break;
                case (int)SocketCommand.TIME_OUT:
                    // Chỉ hiển thị thông báo hết giờ khi thực sự hết giờ
                    if (tmCoolDown.Enabled)
                    {
                        MessageBox.Show(this, "Hết giờ");
                    }
                    break;
                case (int)SocketCommand.QUIT:
                    EndGame();
                    MessageBox.Show(this, "Người chơi đã thoát. Bạn đã thắng.");
                    break;
                default:
                    break;
            }

            Listen();
        }

        #endregion

        private void homeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Hiển thị hộp thoại xác nhận
            DialogResult result = MessageBox.Show("Bạn có muốn về trang chủ không?", "Xác nhận", MessageBoxButtons.OKCancel);

            // Nếu người dùng chọn OK
            if (result == DialogResult.OK)
            {
                // Gửi thông báo cho người chơi còn lại
                socket.Send(new SocketData((int)SocketCommand.QUIT, "", new Point()));

                // Đóng form hiện tại và mở form trang chủ
                this.Hide();
                home hm = new home();
                hm.ShowDialog();
                hm = null;
                this.Close();
            }
            // Nếu người dùng chọn Cancel thì không làm gì
        }

    }
}