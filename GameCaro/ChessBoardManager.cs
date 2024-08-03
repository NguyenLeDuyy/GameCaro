using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Drawing.Text;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        private List<List<Button>> Matrix;

        private event EventHandler playerMared;
        public event EventHandler PlayerMared
        {
            add
            {
                playerMared += value;
            }
            remove
            {
                playerMared -= value;
            }
        }
        private event EventHandler endedGame;
        public event EventHandler EndedGame
        {
            add
            {
                endedGame += value;
            }
            remove
            {
                endedGame -= value;
            }
        }
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
            chessBoard.Enabled = true;
            Matrix = new List<List<Button>>();

            Button oldButton = new Button() { Width = 0, Location = new Point(0, 0) };
            for (int i = 0; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                Matrix.Add(new List<Button>());
                for (int j = 0; j < Cons.CHESS_BOARD_WIDTH; j++)
                {
                    Button btn = new Button()
                    {
                        Width = Cons.CHESS_WIDTH,
                        Height = Cons.CHESS_HEIGHT,
                        Location = new Point(oldButton.Location.X + oldButton.Width, oldButton.Location.Y),
                        BackgroundImageLayout = ImageLayout.Stretch, // Căn chỉnh kích thước của ảnh vừa với button
                        Tag = i.ToString()
                    };

                    btn.Click += btn_Click;

                    oldButton = btn;
                    ChessBoard.Controls.Add(btn);
                    Matrix[i].Add(btn);
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

            if (playerMared != null)
            {
                playerMared(this, new EventArgs());
            }
            if (isEndGame(btn))
            {
                EndGame();
            }
            
        }
        public void EndGame() {
            if (endedGame !=null) 
                endedGame(this,new EventArgs());
        }

        private bool isEndGame(Button btn)
        {
            return isEndHorizontal(btn) || isEndVertical(btn) || isEndPrimary(btn) || isEndSub(btn);
        }
        private Point GetChessPoint(Button btn) {
            

            int vertical = Convert.ToInt32(btn.Tag);
            int horizontal = Matrix[vertical].IndexOf(btn);
            Point point = new Point(horizontal,vertical);

            return point;
        }


        private bool isEndHorizontal(Button btn) //kết thúc ở hàng ngang
        {
            Point point = GetChessPoint(btn);
            int countLeft = 0;
            for (int i = point.X; i >= 0; i--) {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countLeft++;
                }
                else
                       break;
            }
            int countRight = 0;
            for (int i = point.X+1; i < Cons.CHESS_BOARD_WIDTH; i++)
            {
                if (Matrix[point.Y][i].BackgroundImage == btn.BackgroundImage)
                {
                    countRight++;
                }
                else
                    break;
            }
            return countLeft + countRight == 5;
        }
        private bool isEndVertical (Button btn) // kết thúc ở hàng dọc
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = point.Y; i >= 0; i--)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countBottom = 0;
            for (int i = point.Y + 1; i < Cons.CHESS_BOARD_HEIGHT; i++)
            {
                if (Matrix[i][point.X].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }
            return countTop + countBottom == 5;
        }
        private bool isEndPrimary(Button btn) //đường chéo chính
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X ; i++)
            {
                if (point.X - i < 0 || point.Y - i < 0)
                {
                    break;
                }
                if (Matrix[point.Y - i][point.X-i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X + i >= Cons.CHESS_BOARD_WIDTH)
                    { break; }
                if (Matrix[point.Y + i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }
            return countTop + countBottom == 5;
        }
        private bool isEndSub(Button btn) //đường chéo phụ
        {
            Point point = GetChessPoint(btn);
            int countTop = 0;
            for (int i = 0; i <= point.X; i++)
            {
                if (point.X + i >Cons.CHESS_BOARD_WIDTH || point.Y - i < 0)
                {
                    break;
                }
                if (Matrix[point.Y - i][point.X + i].BackgroundImage == btn.BackgroundImage)
                {
                    countTop++;
                }
                else
                    break;
            }
            int countBottom = 0;
            for (int i = 1; i <= Cons.CHESS_BOARD_WIDTH - point.X; i++)
            {
                if (point.Y + i >= Cons.CHESS_BOARD_HEIGHT || point.X - i < 0)
                { break; }
                if (Matrix[point.Y + i][point.X - i].BackgroundImage == btn.BackgroundImage)
                {
                    countBottom++;
                }
                else
                    break;
            }
            return countTop + countBottom == 5;
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
