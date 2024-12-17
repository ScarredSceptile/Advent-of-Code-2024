using System.ComponentModel;

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
                foreach (var lDir in Permute(Dir))
                {
                    var find = visited[i];
                    map = input.Select(n => n.Select(c => (Visited: false, Field: c, Distance: 0, Dir: (X: 0, Y: 1))).ToArray()).ToArray();
                    if (i != 0)
                    {
                        map[find.X][find.Y].Dir = (find.X - visited[i - 1].X, find.Y - visited[i - 1].Y);
                        map[visited[i - 1].X][visited[i - 1].Y].Visited = true;
                        map[visited[i + 1].X][visited[i + 1].Y].Visited = true;
                        if (map[visited[i + 1].X][visited[i + 1].Y].Field == 'E')
                            map[visited[i + 1].X][visited[i + 1].Y].Visited = false;
                    }
                    map[find.X][find.Y].Visited = true;
                    map[find.X][find.Y].Distance = find.Sum;
                    PriorityQueue<(int X, int Y, List<(int, int, int Sum)> visits), int> queue = new();
                    queue.Enqueue((find.X, find.Y, new List<(int, int, int)>() { (find.X, find.Y, find.Sum) }), 0);
                    bool first = true;

                    while (queue.Count > 0)
                    {
                        var pos = queue.Dequeue();
                        if (map[pos.X][pos.Y].Field == 'E')
                        {
                            if (map[pos.X][pos.Y].Distance == sum)
                            {
                                visited.AddRange(pos.visits);
                                visited = visited.GroupBy(n => (n.X, n.Y)).Select(n => n.First()).ToList();
                            }
                            break;
                        }
                        if (map[pos.X][pos.Y].Distance > sum)
                            break;
                        foreach (var dir in lDir)
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
                        first = false;
                    }
                }
            }
            int test = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j].Field == '#')
                        Console.Write('#');
                    else
                    {
                        if (visited.Any(n => n.X == i && n.Y == j))
                        {
                            Console.Write('O');
                            test++;
                        }
                        else
                            Console.Write('.');
                    }
                }
                Console.WriteLine();
            }
            Console.WriteLine(visited.Count());
        }

        private (int, List<(int X, int Y, int Sum)>) GetSum()
        {
            var input = Input.Get("Day16");
            var map = input.Select(n => n.Select(c => (Visited: false, Field: c, Distance: 0, Dir: (X: 0, Y: 1))).ToArray()).ToArray();
            int x = map.Length - 2; int y = 1;
            map[x][y].Visited = true;
            PriorityQueue<(int X, int Y, List<(int, int, int)> visits), int> queue = new();
            List<(int, int, int)> visits = new() { (x, y, 0) };
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
            return (sum, visits);
        }

        static IList<IList<(int X, int Y)>> Permute((int, int)[] nums)
        {
            var list = new List<IList<(int, int)>>();
            return DoPermute(nums, 0, nums.Length - 1, list);
        }

        static IList<IList<(int, int)>> DoPermute((int, int)[] nums, int start, int end, IList<IList<(int, int)>> list)
        {
            if (start == end)
            {
                // We have one of our possible n! solutions,
                // add it to the list.
                list.Add(new List<(int, int)>(nums));
            }
            else
            {
                for (var i = start; i <= end; i++)
                {
                    Swap(ref nums[start], ref nums[i]);
                    DoPermute(nums, start + 1, end, list);
                    Swap(ref nums[start], ref nums[i]);
                }
            }

            return list;
        }

        static void Swap(ref (int, int) a, ref (int, int) b)
        {
            var temp = a;
            a = b;
            b = temp;
        }
    }
}
