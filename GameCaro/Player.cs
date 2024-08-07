﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameCaro
{
    public class Player
    {
        private string name; //Ctrl + R + E 

        public string Name { get => name; set => name = value; }

        private Image sign;
        public Image Sign { get => sign; set => sign = value; }

        public Player(string name, Image sign)
        {
            this.Name = name;
            this.Sign = sign;
        }
    }
}
