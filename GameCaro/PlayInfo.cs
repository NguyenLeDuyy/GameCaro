using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    public class PlayInfo
    {
        private Point point;
        public Point Point
        {
            get { return point; }
            set { point = value; }
        }
        private int currentplayer;
        public int Currentplayer
        { get { return currentplayer; } set { currentplayer = value; } }
        public PlayInfo(Point point, int currentplayer)
        {
            this.point = point;
            this.currentplayer = currentplayer;
        }
    }   
}
