namespace Advent_of_Coding_2024.Days
{
    internal class Day15 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day15").Split("\r\n\r\n").Select(n => n.Split("\r\n").ToArray()).ToArray();
            var movement = string.Join("", input[1]);
            var map = input[0].Select(n => n.ToArray()).ToArray();
            var botPos = FindStart(map);
            foreach (var move in movement)
            {
                var dir = GetDir(move);
                var canMove = CanMove(map, botPos, dir);
                if (canMove.CanMove)
                {
                    for (int i = canMove.boxes; i > 0; i--)
                    {
                        map[botPos.X + (i + 1) * dir.X][botPos.Y + (i + 1) * dir.Y] = 'O';
                        map[botPos.X + i * dir.X][botPos.Y + i * dir.Y] = '.';
                    }
                    botPos.X += dir.X;
                    botPos.Y += dir.Y;
                }
            }
            long sum = 0;
            for (int i = 1; i < map.Length - 1; i++)
            {
                for (int j = 1; j < map[i].Length - 1; j++)
                {
                    if (map[i][j] == 'O')
                        sum += 100 * i + j;
                }
            }
            Console.WriteLine(sum);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day15").Split("\r\n\r\n").Select(n => n.Split("\r\n").ToArray()).ToArray();
            var movement = string.Join("", input[1]);
            var map = input[0].Select(n => string.Join("", n.Select(c => ReplaceTile(c)).ToArray())).Select(n => n.ToArray()).ToArray();
            var botPos = FindStart(map);
            var wallCount = map.Sum(n => n.Where(c => c == '#').Count());

            foreach (var move in movement)
            {
                var dir = GetDir(move);
                var canMove = CanMoveLarge(map, botPos, dir);
                if (canMove.CanMove)
                {
                    if (dir.X == 0)
                    {
                        for (int i = canMove.boxes.Count * 2; i > 0; i--)
                        {
                            map[botPos.X][botPos.Y + (i + 1) * dir.Y] = map[botPos.X][botPos.Y + i * dir.Y];
                            map[botPos.X][botPos.Y + i * dir.Y] = map[botPos.X][botPos.Y + (i - 1) * dir.Y];
                        }
                    }
                    else
                    {
                        var boxes = canMove.boxes.Distinct().ToArray();
                        if (dir.X == 1)
                            boxes = boxes.OrderBy(c => c.X).ToArray();
                        else
                            boxes = boxes.OrderByDescending(c => c.X).ToArray();

                        for (int i = boxes.Length - 1; i >= 0; i--)
                        {
                            map[boxes[i].X + dir.X][boxes[i].Y] = map[boxes[i].X][boxes[i].Y];
                            map[boxes[i].X][boxes[i].Y] = '.';
                        }
                    }
                    botPos.X += dir.X;
                    botPos.Y += dir.Y;
                }
            }

            long sum = 0;
            for (int i = 0; i < map.Length; i++)
            {
                for (int j = 0; j < map[i].Length; j++)
                {
                    if (map[i][j] == '[')
                        sum += 100 * (long)i + j;
                }
            }
            Console.WriteLine(sum);
        }

        private string ReplaceTile(char tile)
        {
            if (tile == '#')
                return "##";
            else if (tile == 'O')
                return "[]";
            else if (tile == '.')
                return "..";
            else if (tile == '@')
                return "@.";
            return string.Empty;
        }

        private (bool CanMove, int boxes) CanMove(char[][] map, (int X, int Y) pos, (int X, int Y) dir)
        {
            int boxes = 0;
            while (true)
            {
                pos.X += dir.X;
                pos.Y += dir.Y;
                if (map[pos.X][pos.Y] == '.')
                    return (true, boxes);
                else if (map[pos.X][pos.Y] == 'O')
                    boxes++;
                else
                    return (false, 0);
            }
        }

        private (bool CanMove, List<(int X, int Y)> boxes) CanMoveLarge(char[][] map, (int X, int Y) pos, (int X, int Y) dir)
        {
            List<(int, int)> boxes = new();
            if (dir.X == 0)
            {
                var x = pos.X;
                while (true)
                {
                    pos.Y += dir.Y;
                    if (map[x][pos.Y] == '.')
                        return (true, boxes);
                    else if (map[x][pos.Y] == '[')
                    {
                        boxes.Add((x, pos.Y));
                        pos.Y += dir.Y;
                    }
                    else if (map[x][pos.Y] == ']')
                    {
                        pos.Y += dir.Y;
                        boxes.Add((x, pos.Y));
                    }
                    else
                        return (false, boxes);
                }
            }
            else
            {
                var nextPos = map[pos.X + dir.X][pos.Y];
                if (nextPos == '.')
                    return (true, boxes);
                else if (nextPos == '[')
                {
                    boxes.Add((pos.X + dir.X, pos.Y));
                    boxes.Add((pos.X + dir.X, pos.Y + 1));
                    var direct = CanMoveLarge(map, (pos.X + dir.X, pos.Y), dir);
                    var side = CanMoveLarge(map, (pos.X + dir.X, pos.Y + 1), dir);
                    boxes.AddRange(direct.boxes);
                    boxes.AddRange(side.boxes);
                    return (direct.CanMove && side.CanMove, boxes);
                }
                else if (nextPos == ']')
                {
                    boxes.Add((pos.X + dir.X, pos.Y));
                    boxes.Add((pos.X + dir.X, pos.Y - 1));
                    var direct = CanMoveLarge(map, (pos.X + dir.X, pos.Y), dir);
                    var side = CanMoveLarge(map, (pos.X + dir.X, pos.Y - 1), dir);
                    boxes.AddRange(direct.boxes);
                    boxes.AddRange(side.boxes);
                    return (direct.CanMove && side.CanMove, boxes);
                }
                return (false, boxes);
            }
        }

        private (int X, int Y) FindStart(char[][] input)
        {
            for (int i = 1; i < input.Length - 1; i++)
            {
                for (int j = 1; j < input[i].Length - 1; j++)
                {
                    if (input[i][j] == '@')
                    {
                        input[i][j] = '.';
                        return (i, j);
                    }
                }
            }
            return (0, 0);
        }

        private (int X, int Y) GetDir(char dir)
        {
            if (dir == '>')
            {
                return (0, 1);
            }
            else if (dir == 'v')
            {
                return (1, 0);
            }
            else if (dir == '<')
            {
                return (0, -1);
            }
            else if (dir == '^')
            {
                return (-1, 0);
            }
            return (0, 0);
        }
    }
}
