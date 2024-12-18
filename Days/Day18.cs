using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day18 : Day
    {
        private readonly (int X, int Y)[] Dir = new[]
        {
            (0, 1),
            (0, -1),
            (1, 0),
            (-1, 0)
        };

        public void Star1()
        {
            var input = Input.Get("Day18");
            (bool Visited, char Field, int Distance)[][] map = new (bool, char, int)[71][];
            for (int i = 0; i <  map.Length; i++)
            {
                map[i] = new (bool, char, int)[71];
                for (int j = 0; j < map[i].Length; j++)
                { map[i][j] = (false, '.', 0); }
            }
            for (int i = 0; i < 1024; i++)
            {
                var place = input[i].Split(',').Select(int.Parse).ToArray();
                map[place[0]][place[1]].Field = '#';
            }
            Console.WriteLine(GetSum(map));
        }

        public void Star2()
        {
            var input = Input.Get("Day18");
            (bool Visited, char Field, int Distance)[][] map = new (bool, char, int)[71][];
            for (int i = 0; i <  map.Length; i++)
            {
                map[i] = new (bool, char, int)[71];
                for (int j = 0; j < map[i].Length; j++)
                { map[i][j] = (false, '.', 0); }
            }
            int[] place = new int[2];
            for (int i = 0; i < 1024; i++)
            {
                place = input[i].Split(',').Select(int.Parse).ToArray();
                map[place[0]][place[1]].Field = '#';
            }
            for (int i = 1024; i < input.Length; i++)
            {
                place = input[i].Split(',').Select(int.Parse).ToArray();
                map[place[0]][place[1]].Field = '#';
                var sum = GetSum(map.Select(n => n.ToArray()).ToArray());
                if (sum == 0)
                    break;
            }
            Console.WriteLine(string.Join(",", place));
        }

        private bool InRange((int X, int Y) pos)
        {
            return pos.X < 71 && pos.X >= 0 && pos.Y < 71 && pos.Y >= 0;
        }

        private int GetSum((bool Visited, char Field, int Distance)[][] map)
        {
            map[0][0].Visited = true;
            PriorityQueue<(int X, int Y), int> queue = new();
            queue.Enqueue((0, 0), 0);
            int sum = 0;

            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                if (pos.X == 70 && pos.Y == 70)
                {
                    sum = map[pos.X][pos.Y].Distance;
                    break;
                }
                foreach (var dir in Dir)
                {
                    var x = pos.X + dir.X;
                    var y = pos.Y + dir.Y;
                    if (!InRange((x, y))) continue;
                    if (map[x][y].Visited == false && map[x][y].Field != '#')
                    {
                        map[x][y].Distance = map[pos.X][pos.Y].Distance + 1;
                        queue.Enqueue((x, y), map[x][y].Distance);
                        map[x][y].Visited = true;
                    }
                }
            }

            return sum;
        }
    }
}
