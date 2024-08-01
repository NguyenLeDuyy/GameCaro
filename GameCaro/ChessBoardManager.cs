using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    public class ChessBoardManager
    {
        
        #region Properties
        private Panel chessBoard; // Khởi tạo 1 biến chessBoard có KDL là Panel
        // Giúp đảm bảo tính đóng gói khi thay vì trực tiếp làm việc với thuộc tính chessBoard (private) thì 
        // thông qua cách khai báo ChessBoard này ta có thể tương tác được nhưng ko ảnh hưởng đến chessBoard
        public Panel ChessBoard { get => chessBoard; set => chessBoard = value; }
        
        private List<Player> player;
        public List<Player> Player { get => player; set => player = value; }

        private int currentplayer;
        public int Currentplayer { get => currentplayer; set => currentplayer = value; }

        private TextBox playerName;
        public TextBox PlayerName { get => playerName; set => playerName = value; }

        private PictureBox playerSign;
        public PictureBox PlayerSign { get => playerSign; set => playerSign = value; }
        #endregion

        #region Initialize
        public ChessBoardManager(Panel chessBoard, TextBox playerName, PictureBox sign) // Constructor
        {
            this.ChessBoard = chessBoard; // con trỏ this trỏ tới thuộc tính ChessBoard => copy dữ liệu của chessBoard
            this.PlayerName = playerName;
            this.PlayerSign = sign;
            this.Player = new List<Player>()
            {
                new Player("Nhom1_LTM", Image.FromFile(Application.StartupPath + "\\Resources\\21.png")),
                new Player("Bui_Duong_The", Image.FromFile(Application.StartupPath + "\\Resources\\1.png")),
            };
            currentplayer = 0;
            SwitchPlayer();
        }
        #endregion

        #region Methods
        public void DrawChessBoard()
        {
            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch, // Căn chỉnh kích thước của ảnh vừa với button
                    };

                    btn.Click += btn_Click;

                    oldButton = btn;
                    ChessBoard.Controls.Add(btn);
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }

        void btn_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn.BackgroundImage != null)
            {
                return;
            }
            Sign(btn);
            SwitchPlayer();

        }
        private void Sign(Button btn)
        {
            btn.BackgroundImage = Player[Currentplayer].Sign;
            Currentplayer = Currentplayer == 1 ? 0 : 1;
        }
        private void SwitchPlayer()
        {
            PlayerName.Text = Player[Currentplayer].Name;
            PlayerSign.Image = Player[Currentplayer].Sign;
        }
        #endregion

    }
}
