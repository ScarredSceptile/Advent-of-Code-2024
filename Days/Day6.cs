using Advent_of_Coding_2024.Utils;
using Microsoft.Win32.SafeHandles;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day6 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day6");
            bool[][] visited = new bool[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                visited[i] = new bool[input[i].Length];
            }
            var pos = FindStart(input);
            var dir = new Pos(-1, 0);
            while (true)
            {
                visited[pos.X][pos.Y] = true;
                var nextPos = pos + dir;
                if (!InRange(nextPos, input))
                    break;
                if (input[nextPos.X][nextPos.Y] == '#')
                {
                    dir.Rotate();
                }
                else
                {
                    pos += dir;
                }
            }
            Console.WriteLine(visited.Sum(n => n.Where(v => v).Count()));
        }

        public void Star2()
        {
            var input = Input.Get("Day6");
            var origPos = FindStart(input);
            int loops = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#' || new Pos(i, j) == origPos) continue;
                    List<(Pos pos, Pos dir)> turns = new();
                    StringBuilder sb = new StringBuilder(input[i]);
                    var map = input.ToArray();
                    sb[j] = '#';
                    map[i] = sb.ToString();
                    var dir = new Pos(-1, 0);
                    var pos = new Pos(origPos);

                    while (true)
                    {
                        var nextPos = pos + dir;
                        if (!InRange(nextPos, input))
                            break;
                        if (map[nextPos.X][nextPos.Y] == '#')
                        {
                            if (turns.Any(n => n.pos == pos && n.dir == dir))
                            {
                                loops++;
                                break;
                            }
                            turns.Add((new Pos(pos), new Pos(dir)));
                            dir.Rotate();
                        }
                        else
                        {
                            pos += dir;
                        }
                    }
                }
            }
            Console.WriteLine(loops);
        }

        private bool InRange(Pos pos, string[] input)
        {
            return pos.X < input.Length && pos.X >= 0 && pos.Y < input[0].Length && pos.Y >= 0;
        }

        private Pos FindStart(string[] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '^')
                    {
                        return new Pos(i, j);
                    }
                }
            }
            return new Pos(0, 0);
        }

    }
}
