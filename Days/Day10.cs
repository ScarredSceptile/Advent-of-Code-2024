using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day10 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day10").Select(x => x.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            int sumPeaks = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j]  == 0)
                        sumPeaks += GetPeaks(0, i, j, input).Distinct().Count();
                }
            }
            Console.WriteLine(sumPeaks);
        }

        public void Star2()
        {
            var input = Input.Get("Day10").Select(x => x.Select(c => int.Parse(c.ToString())).ToArray()).ToArray();
            int sumPeaks = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j]  == 0)
                        sumPeaks += GetPeaks(0, i, j, input).Count();
                }
            }
            Console.WriteLine(sumPeaks);
        }

        private List<(int X, int Y)> GetPeaks(int height, int X, int Y, int[][] map)
        {
            List<(int X, int Y)> peaksReached = new();
            if (height == 9)
            {
                peaksReached.Add((X, Y));
                return peaksReached;
            }
            if (X - 1 >= 0)
            {
                if (map[X - 1][Y] == height + 1)
                    peaksReached.AddRange(GetPeaks(height + 1, X - 1, Y, map));
            }
            if (Y - 1 >= 0)
            {
                if (map[X][Y - 1] == height + 1)
                    peaksReached.AddRange(GetPeaks(height + 1, X, Y - 1, map));
            }
            if (X + 1 < map.Length)
            {
                if (map[X + 1][Y] == height +1)
                    peaksReached.AddRange(GetPeaks(height + 1, X + 1, Y, map));
            }
            if (Y + 1 < map[X].Length)
            {
                if (map[X][Y + 1] == height +1)
                    peaksReached.AddRange(GetPeaks(height + 1, X, Y + 1, map));
            }
            return peaksReached;
        }
    }
}
