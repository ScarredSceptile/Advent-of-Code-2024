using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Utils
{
    internal class Pos
    {
        public int X;
        public int Y;

        public Pos(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Pos(Pos pos)
        {
            X = pos.X;
            Y = pos.Y;
        }

        public static Pos operator +(Pos a, Pos b)
        {
            var x = a.X + b.X;
            var y = a.Y + b.Y;
            return new Pos(x, y);
        }

        public static bool operator ==(Pos a, Pos b)
        {
            return a.X == b.X && a.Y == b.Y;
        }
        public static bool operator !=(Pos a, Pos b)
        {
            return !(a == b);
        }

        public void Rotate()
        {
            if (X == -1)
            {
                X = 0; Y = 1;
            }
            else if(Y == 1)
            {
                Y = 0; X = 1;
            }
            else if(X == 1)
            {
                X = 0; Y = -1;
            }
            else
            {
                X = -1; Y = 0;
            }
        }
    }
}
