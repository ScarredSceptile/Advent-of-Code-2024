using System.Runtime.CompilerServices;
using System.Text;

namespace Advent_of_Coding_2024.Days
{
    internal class Day6 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day6").Select(n => n.ToArray()).ToArray();
            Console.WriteLine(GetVisits(input, out _).Sum(n => n.Where(v => v).Count()));
        }

        public void Star2()
        {
            var input = Input.Get("Day6").Select(n => n.ToArray()).ToArray();
            var visited = GetVisits(input, out var origPos);
            int loops = 0;
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '#' || (i, j) == origPos || visited[i][j] == false) continue;
                    List<((int X, int Y) pos, (int X, int Y) dir)> turns = new();
                    input[i][j] = '#';
                    var dir = (X: -1, Y: 0);
                    var pos = origPos;

                    while (true)
                    {
                        var x = pos.X + dir.X;
                        var y = pos.Y + dir.Y;
                        var nextPos = (X: x, Y: y);
                        if (!InRange(nextPos, input))
                            break;
                        if (input[nextPos.X][nextPos.Y] == '#')
                        {
                            if (turns.Any(n => n.pos == pos && n.dir == dir))
                            {
                                loops++;
                                break;
                            }
                            turns.Add((pos, dir));
                            dir = Rotate(dir);
                        }
                        else
                        {
                            pos.X += dir.X;
                            pos.Y += dir.Y;
                        }
                    }

                    input[i][j] = '.';
                }
            }
            Console.WriteLine(loops);
        }

        private bool[][] GetVisits(char[][] input, out (int X, int Y) start)
        {
            bool[][] visited = new bool[input.Length][];
            for (int i = 0; i < input.Length; i++)
            {
                visited[i] = new bool[input[i].Length];
            }
            var pos = FindStart(input);
            start = pos;
            var dir = (X: -1, Y: 0);
            while (true)
            {
                visited[pos.X][pos.Y] = true;
                var x = pos.X + dir.X;
                var y = pos.Y + dir.Y;
                var nextPos = (X: x, Y: y);
                if (!InRange(nextPos, input))
                    break;
                if (input[nextPos.X][nextPos.Y] == '#')
                {
                    dir = Rotate(dir);
                }
                else
                {
                    pos.X += dir.X;
                    pos.Y += dir.Y;
                }
            }
            return visited;
        }

        private bool InRange((int X, int Y) pos, char[][] input)
        {
            return pos.X < input.Length && pos.X >= 0 && pos.Y < input[0].Length && pos.Y >= 0;
        }

        private (int X, int Y) FindStart(char[][] input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                for (int j = 0; j < input[i].Length; j++)
                {
                    if (input[i][j] == '^')
                    {
                        return (i, j);
                    }
                }
            }
            return (0, 0);
        }

        private (int X, int Y) Rotate((int X, int Y) dir)
        {
            if (dir.X == -1)
            {
                return (0, 1);
            }
            else if (dir.Y == 1)
            {
                return (1, 0);
            }
            else if (dir.X == 1)
            {
                return (0, -1);
            }
            else
            {
                return (-1, 0);
            }
        }


    }
}
