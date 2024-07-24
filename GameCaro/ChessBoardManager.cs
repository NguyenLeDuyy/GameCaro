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
        #endregion

        #region Initialize
        public ChessBoardManager(Panel chessBoard) // Constructor
        {
            this.ChessBoard = chessBoard; // con trỏ this trỏ tới thuộc tính ChessBoard => copy dữ liệu của chessBoard
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
                    };
                    oldButton = btn;
                    ChessBoard.Controls.Add(btn);
                }
                oldButton.Location = new Point(0, oldButton.Location.Y + Cons.CHESS_HEIGHT);
                oldButton.Width = 0;
                oldButton.Height = 0;
            }
        }
        #endregion

    }
}
