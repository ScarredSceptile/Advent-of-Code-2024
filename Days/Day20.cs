using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day20 : Day
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
            var (track, maxSize) = GetTrack();
            int cheats = 0;
            foreach (var pos in track)
            {
                foreach (var dir in Dir)
                {
                    var newPos = (X: pos.X + dir.X * 2, Y: pos.Y + dir.Y * 2);
                    if (InRange(newPos, maxSize) == false) continue;
                    var nextPos = track.FirstOrDefault(n => n.X == newPos.X && n.Y == newPos.Y);
                    if (nextPos == null) continue;

                    var distSpared = nextPos.Sum - (pos.Sum + 2);
                    if (distSpared >= 100)
                        cheats++;
                }
            }
            Console.WriteLine(cheats);
        }

        public void Star2()
        {
            var (track, maxSize) = GetTrack();
            int cheats = 0;
            foreach (var pos in track)
            {
                foreach (var newPos in ManhattanDistance(pos, 20))
                {
                    if (InRange((newPos.X, newPos.Y), maxSize) == false) continue;
                    var nextPos = track.FirstOrDefault(n => n.X == newPos.X && n.Y == newPos.Y);
                    if (nextPos == null) continue;

                    var distSpared = nextPos.Sum - (pos.Sum + newPos.Dist);
                    if (distSpared >= 100)
                        cheats++;
                }
            }
            Console.WriteLine(cheats);
        }

        private (Tile[], int) GetTrack()
        {
            var input = Input.Get("Day20");
            (bool Visited, char Field, int Distance, (int X, int Y) Dir)[][] map = input.Select(n => n.Select(c => (Visited: false, Field: c, Distance: 0, Dir: (X: 0, Y: 1))).ToArray()).ToArray();
            var (x, y) = FindStart(map.Select(n => n.Select(c => c.Field).ToArray()).ToArray());
            map[x][y].Visited = true;
            PriorityQueue<(int X, int Y), int> queue = new();
            queue.Enqueue((x, y), 0);
            List<Tile> track = new() { new Tile(0, x, y) };

            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                foreach (var dir in Dir)
                {
                    x = pos.X + dir.X;
                    y = pos.Y + dir.Y;
                    if (map[x][y].Visited == false && map[x][y].Field != '#')
                    {
                        map[x][y].Distance = map[pos.X][pos.Y].Distance + 1;
                        var tile = new Tile(map[x][y].Distance, x, y);
                        track.Add(tile);
                        queue.Enqueue((x, y), map[x][y].Distance);
                        map[x][y].Visited = true;
                        map[x][y].Dir = dir;
                    }
                }
            }
            return (track.ToArray(), map.Length);
        }

        private List<(int X, int Y, int Dist)> ManhattanDistance(Tile pos,  int distance)
        {
            var list = new List<(int, int, int)>();

            for (int i = 2; i <= distance; i++)
            {
                var nextPos = (pos.X,Y:  pos.Y + i, i);
                for (int j = 0; j < i; j++)
                {
                    nextPos.X -= 1;
                    nextPos.Y -= 1;
                    list.Add(nextPos);
                }
                for (int j = 0; j < i; j++)
                {
                    nextPos.X += 1;
                    nextPos.Y -= 1;
                    list.Add(nextPos);
                }
                for (int j = 0; j < i; j++)
                {
                    nextPos.X += 1;
                    nextPos.Y += 1;
                    list.Add(nextPos);
                }
                for (int j = 0; j < i; j++)
                {
                    nextPos.X -= 1;
                    nextPos.Y += 1;
                    list.Add(nextPos);
                }
            }
            return list;
        }

        private bool InRange((int X, int Y) pos, int max)
        {
            return pos.X < max && pos.X >= 0 && pos.Y < max && pos.Y >= 0;
        }

        private (int, int) FindStart(char[][] map)
        {
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == 'S')
                        return (i, j);
                }
            }
            return (-1, -1);
        }

        private class Tile
        {
            public int Sum;
            public int X;
            public int Y;
            public Tile(int sum, int x, int y)
            {
                Sum=sum;
                X=x;
                Y=y;
            }
        }
    }
}
