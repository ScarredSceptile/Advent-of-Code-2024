using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day14 : Day
    {
        private readonly int X = 101;
        private readonly int Y = 103;

        public void Star1()
        {
            var input = Input.Get("Day14").Select(n => n.Split(' ')).ToArray();
            int tr = 0, tl = 0, br = 0, bl = 0;
            foreach (var item in input)
            {
                var pos = item[0].Split('=')[1].Split(',').Select(long.Parse).ToArray();
                var vec = item[1].Split('=')[1].Split(',').Select(long.Parse).ToArray();
                var x = Mod((int)(pos[0] + vec[0] * 100), X);
                var y = Mod((int)(pos[1] + vec[1] * 100), Y);

                if (x < X / 2)
                {
                    if (y < Y / 2)
                        tl++;
                    else if (y > Y / 2)
                        tr++;
                }
                else if (x > X/2)
                {
                    if (y < Y / 2)
                        bl++;
                    else if (y > Y / 2)
                        br++;
                }
            }
            Console.WriteLine(tr * tl * br * bl);
        }

        public void Star2()
        {
            var input = Input.Get("Day14").Select(n => n.Split(' ')).ToArray();
            List<((int X, int Y) Pos, (int X, int Y) Vec)> bots = new();
            foreach (var item in input)
            {
                var pos = item[0].Split('=')[1].Split(',').Select(int.Parse).ToArray();
                var vec = item[1].Split('=')[1].Split(',').Select(int.Parse).ToArray();
                bots.Add(((pos[0], pos[1]), (vec[0], vec[1])));
            }

            Parallel.For(1, X * Y, i =>
            {
                var map = new Bitmap(X, Y);
                foreach (var bot in bots)
                {
                    var x = Mod((bot.Pos.X + bot.Vec.X * i), X);
                    var y = Mod((bot.Pos.Y + bot.Vec.Y * i), Y);
                    map.SetPixel(x, y, Color.Green);
                    Input.SaveImage(map, i.ToString());
                }
            });
        }

        private int Mod(int x, int m)
        {
            return (x%m + m)%m;
        }
    }
}
