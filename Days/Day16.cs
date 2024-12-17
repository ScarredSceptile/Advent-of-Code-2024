using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Advent_of_Coding_2024.Days
{
    internal class Day16 : Day
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
            Console.WriteLine(GetSum().Item1);
        }

        public void Star2()
        {
            var input = Input.Get("Day16");
            var map = input.Select(n => n.Select(c => (Visited: false, Field: c, Distance: 0, Dir: (X: 0, Y: 1))).ToArray()).ToArray();
            var (sum, visited) = GetSum();
            for (int i = 0; i < visited.Count - 1; i++)
            {
                var find = visited[i];
                if (find.Next.Count == 0)
                    continue;
                map = input.Select(n => n.Select(c => (Visited: false, Field: c, Distance: 0, Dir: (X: 0, Y: 1))).ToArray()).ToArray();
                if (i != 0)
                {
                    map[find.X][find.Y].Dir = (find.X - find.Previous.X, find.Y - find.Previous.Y);
                    map[find.Previous.X][find.Previous.Y].Visited = true;
                }
                foreach (var next in find.Next)
                {
                    map[next.X][next.Y].Visited = true;
                }
                map[find.X][find.Y].Visited = true;
                map[find.X][find.Y].Distance = find.Sum;
                PriorityQueue<(int X, int Y, List<(int X, int Y, int Sum)> visits), int> queue = new();
                queue.Enqueue((find.X, find.Y, new List<(int, int, int)>() { (find.X, find.Y, find.Sum) }), 0);

                while (queue.Count > 0)
                {
                    var pos = queue.Dequeue();
                    if (map[pos.X][pos.Y].Field == 'E')
                    {
                        if (map[pos.X][pos.Y].Distance == sum)
                        {
                            List<Tile> tiles = new List<Tile>() { find };
                            for (int j = 1; j < pos.visits.Count -1; j++)
                            {
                                var newTile = new Tile(tiles[j - 1], new List<Tile>(), pos.visits[j].Sum, pos.visits[j].X, pos.visits[j].Y);
                                tiles[j-1].Next.Add(newTile);
                                tiles.Add(newTile);
                            }
                            visited.AddRange(tiles);
                            visited = visited.Distinct(new TileComparer()).ToList();
                            i--;
                        }
                        break;
                    }
                    if (map[pos.X][pos.Y].Distance > sum)
                        break;
                    foreach (var dir in Dir)
                    {
                        var x = pos.X + dir.X;
                        var y = pos.Y + dir.Y;
                        if (map[x][y].Visited == false && map[x][y].Field != '#')
                        {
                            map[x][y].Distance = map[pos.X][pos.Y].Distance + (map[pos.X][pos.Y].Dir == dir ? 1 : 1001);
                            var v = pos.visits.ToList();
                            v.Add((x, y, map[x][y].Distance));
                            queue.Enqueue((x, y, v), map[x][y].Distance);
                            map[x][y].Visited = true;
                            map[x][y].Dir = dir;
                        }
                    }
                }

            }
            int test = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j].Field != '#' && visited.Any(n => n.X == i && n.Y == j))
                        test++;
                }
            }
            Console.WriteLine(test);
        }

        private (int, List<Tile>) GetSum()
        {
            var input = Input.Get("Day16");
            var map = input.Select(n => n.Select(c => (Visited: false, Field: c, Distance: 0, Dir: (X: 0, Y: 1))).ToArray()).ToArray();
            int x = map.Length - 2; int y = 1;
            map[x][y].Visited = true;
            PriorityQueue<(int X, int Y, List<(int, int, int)> visits), int> queue = new();
            List<(int X, int Y, int Sum)> visits = new() { (x, y, 0) };
            queue.Enqueue((x, y, visits), 0);
            int sum = 0;

            while (queue.Count > 0)
            {
                var pos = queue.Dequeue();
                if (map[pos.X][pos.Y].Field == 'E')
                {
                    sum = map[pos.X][pos.Y].Distance;
                    visits = pos.visits;
                    break;
                }
                foreach (var dir in Dir)
                {
                    x = pos.X + dir.X;
                    y = pos.Y + dir.Y;
                    if (map[x][y].Visited == false && map[x][y].Field != '#')
                    {
                        map[x][y].Distance = map[pos.X][pos.Y].Distance + (map[pos.X][pos.Y].Dir == dir ? 1 : 1001);
                        var v = pos.visits.ToList();
                        v.Add((x, y, map[x][y].Distance));
                        queue.Enqueue((x, y, v), map[x][y].Distance);
                        map[x][y].Visited = true;
                        map[x][y].Dir = dir;
                    }
                }
            }
            List<Tile> tiles = new List<Tile>() { new Tile(null, new List<Tile>(), visits[0].Sum, visits[0].X, visits[0].Y) };
            for (int i = 1; i < visits.Count; i++)
            {
                var newTile = new Tile(tiles[i - 1], new List<Tile>(), visits[i].Sum, visits[i].X, visits[i].Y);
                tiles[i-1].Next.Add(newTile);
                tiles.Add(newTile);
            }
            return (sum, tiles);
        }

        private class Tile
        {
            public Tile Previous;
            public List<Tile> Next;
            public int Sum;
            public int X;
            public int Y;
            public Tile(Tile previous, List<Tile> next, int sum, int x, int y)
            {
                Previous=previous;
                Next=next;
                Sum=sum;
                X=x;
                Y=y;
            }
        }

        private class TileComparer : IEqualityComparer<Tile>
        {
            public bool Equals(Tile? x, Tile? y)
            {
                return x.X == y.X && x.Y == y.Y && x.Sum == y.Sum;
            }

            public int GetHashCode([DisallowNull] Tile obj)
            {
                var hashX = obj.X.GetHashCode();
                var hashY = obj.Y.GetHashCode();
                var hashSum = obj.Sum.GetHashCode();
                return hashX ^ hashY ^ hashSum;
            }
        }
    }
}
