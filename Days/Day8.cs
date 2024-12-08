using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day8 : Day
    {
        public void Star1()
        {
            var (antinodes, antennas) = GetInputs();
            var frequencies = antennas.GroupBy(n => n.Freq).ToArray();
            foreach (var frequency in frequencies)
            {
                var freq = frequency.ToArray();
                for (int i = 0; i < frequency.Count() - 1; i++)
                {
                    for (int j = i+1; j < frequency.Count(); j++)
                    {
                        var distX = freq[i].X - freq[j].X;
                        var distY = freq[i].Y - freq[j].Y;
                        var iAnti = (X: freq[i].X + distX, Y: freq[i].Y + distY);
                        var jAnti = (X: freq[j].X - distX, Y: freq[j].Y - distY);

                        if (InRange(iAnti, antinodes))
                            antinodes[iAnti.X][iAnti.Y] = true;
                        if (InRange(jAnti, antinodes))
                            antinodes[jAnti.X][jAnti.Y] = true;
                    }
                }
            }
            Console.WriteLine(antinodes.Sum(n => n.Count(c => c)));
        }

        public void Star2()
        {
            var (antinodes, antennas) = GetInputs();
            var frequencies = antennas.GroupBy(n => n.Freq).ToArray();
            foreach (var frequency in frequencies)
            {
                var freq = frequency.ToArray();
                for (int i = 0; i < frequency.Count() - 1; i++)
                {
                    for (int j = i+1; j < frequency.Count(); j++)
                    {
                        var distX = freq[i].X - freq[j].X;
                        var distY = freq[i].Y - freq[j].Y;
                        var iAnti = (X: freq[i].X + distX, Y: freq[i].Y + distY);
                        var jAnti = (X: freq[j].X - distX, Y: freq[j].Y - distY);
                        antinodes[freq[i].X][freq[i].Y] = true;
                        antinodes[freq[j].X][freq[j].Y] = true;

                        while (InRange(iAnti, antinodes))
                        {
                            antinodes[iAnti.X][iAnti.Y] = true;
                            iAnti = (X: iAnti.X + distX, Y: iAnti.Y + distY);
                        }
                        while (InRange(jAnti, antinodes))
                        {
                            antinodes[jAnti.X][jAnti.Y] = true;
                            jAnti = (X: jAnti.X - distX, Y: jAnti.Y - distY);
                        }
                    }
                }
            }
            Console.WriteLine(antinodes.Sum(n => n.Count(c => c)));
        }

        private bool InRange((int X, int Y) pos, bool[][] input)
        {
            return pos.X < input.Length && pos.X >= 0 && pos.Y < input[0].Length && pos.Y >= 0;
        }

        private (bool[][], List<(char Freq, int X, int Y)>) GetInputs()
        {
            var input = Input.Get("Day8");
            bool[][] antinodes = new bool[input.Length][];
            List<(char Freq, int X, int Y)> antennas = new();
            for (int i = 0; i < input.Length; i++)
            {
                antinodes[i] = new bool[input[i].Length];
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] != '.')
                    {
                        antennas.Add((input[i][j], i, j));
                    }
                }
            }
            return (antinodes, antennas);
        }
    }
}
