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
            MessageBox.Show("Trò chơi kết thúc!");
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
        }

        private void ChessBoard_PlayerSigned(object sender, ButtonClickEvent e)
        {
            tmCoolDown.Start();
            pnlChessBoard.Enabled = false;
            prcbCoolDown.Value = 0;
            socket.Send(new SocketData((int)SocketCommand.SEND_POINT, "", e.ClickedPoint));

            Listen();
        }

        private void ChessBoard_EndedGame(object? sender, EventArgs e)
        {
            EndGame();
        }

        private void tmCoolDown_Tick(object sender, EventArgs e)
        {
            prcbCoolDown.PerformStep();

            if (prcbCoolDown.Value >= prcbCoolDown.Maximum)
            {
                EndGame();
            }
        }

        private void newGameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NewGame();
        }

        private void undoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Undo();
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Quit();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc muốn thoát?", "Thông báo", MessageBoxButtons.OKCancel) != DialogResult.OK)
                e.Cancel = true;
        }

        private void btnLAN_Click(object sender, EventArgs e)
        {
            socket.IP = txbIP.Text;

            if (!socket.ConnectServer())
            {
                socket.isServer = true;
                pnlChessBoard.Enabled = true;
                socket.CreateServer();
            }
            else
            {
                socket.isServer = false;
                pnlChessBoard.Enabled = false;
                Listen();
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
                    break;                
                case (int)SocketCommand.SEND_POINT:
                    // 1. Xử lý lỗi cross-thread do multi-thread (prcbCoolDown đang chạy trong 1 luồng khác - luồng giao diện) và một luồng khác gọi đến
                    // 2. Do prcbCoolDown thực hiện việc Start nằm trong 1 luồng khác => để giao diện chạy mượt thì phải đặt vào Invoke
                    this.Invoke((MethodInvoker)(() =>
                    {
                        pnlChessBoard.Enabled = true;
                        prcbCoolDown.Value = 0;
                        tmCoolDown.Start();
                        ChessBoard.OtherPlayerSign(data.Point);
                    }));
                    break;
                case (int)SocketCommand.UNDO:
                    break;                  
                case (int)SocketCommand.END_GAME:

                    break;                
                case (int)SocketCommand.QUIT:
                    break;
                default:

                    break;
            }

            Listen();
        }
        #endregion
    }
}