namespace Advent_of_Coding_2024.Days
{
    internal class Day12 : Day
    {
        public void Star1()
        {
            var spreadMap = GetMap().GroupBy(n => n.Plant).ToArray();
            int sumAreas = 0;
            foreach (var plant in spreadMap)
            {
                var locations = plant.ToList();
                while (locations.Count > 0)
                {
                    var area = new List<(char Plant, int X, int Y)>() { locations[0] };
                    locations.RemoveAt(0);
                    for (int i = 0; i < area.Count; i++)
                    {
                        var nextTo = locations.Where(n => NextTo(n, area[i])).ToArray();
                        area.AddRange(nextTo);
                        foreach (var location in nextTo)
                            locations.Remove(location);
                    }
                    sumAreas += GetPrice(area);
                }
            }
            Console.WriteLine(sumAreas);
        }

        public void Star2()
        {
            var spreadMap = GetMap().GroupBy(n => n.Plant).ToArray();
            int sumAreas = 0;
            foreach (var plant in spreadMap)
            {
                var locations = plant.ToList();
                while (locations.Count > 0)
                {
                    var area = new List<(char Plant, int X, int Y)>() { locations[0] };
                    locations.RemoveAt(0);
                    for (int i = 0; i < area.Count; i++)
                    {
                        var nextTo = locations.Where(n => NextTo(n, area[i])).ToArray();
                        area.AddRange(nextTo);
                        foreach (var location in nextTo)
                            locations.Remove(location);
                    }
                    sumAreas += GetPricesSides(area);
                }
            }
            Console.WriteLine(sumAreas);
        }

        private List<(char Plant, int X, int Y)> GetMap()
        {
            var input = Input.Get("Day12");
            List<(char, int, int)> values = new();
            for (int i = 0; i < input.Length; i++)
                for (int j = 0; j < input[i].Length; j++)
                    values.Add((input[i][j], i, j));
            return values;
        }

        private bool NextTo((char, int X, int Y) plant, (char, int X, int Y) other)
        {
            if ((plant.X == other.X - 1 || plant.X == other.X + 1) && plant.Y == other.Y)
                return true;
            if ((plant.Y == other.Y - 1 || plant.Y == other.Y + 1) && plant.X == other.X)
                return true;
            return false;
        }

        private int GetPrice(List<(char, int X, int Y)> area)
        {
            int fences = 0;
            for (int i = 0; i < area.Count; i++)
            {
                if (area.Any(n => n.X == area[i].X && n.Y == area[i].Y + 1) == false)
                    fences++;
                if (area.Any(n => n.X == area[i].X && n.Y == area[i].Y - 1) == false)
                    fences++;
                if (area.Any(n => n.X == area[i].X + 1 && n.Y == area[i].Y) == false)
                    fences++;
                if (area.Any(n => n.X == area[i].X - 1 && n.Y == area[i].Y) == false)
                    fences++;
            }
            return fences * area.Count;
        }

        private int GetPricesSides(List<(char, int X, int Y)> area)
        {
            List<(List<(int X, int Y)> list, (int X, int Y) side)> fences = new();
            for (int i = 0; i < area.Count; i++)
            {
                if (area.Any(n => n.X == area[i].X && n.Y == area[i].Y + 1) == false)
                {
                    if (fences.Any(n => n.list.Any(f => f.X == area[i].X && f.Y == area[i].Y + 1) && n.side == (0,1)) == false)
                        fences.Add(AddSide(area[i], area, 0, 1));
                }
                if (area.Any(n => n.X == area[i].X && n.Y == area[i].Y - 1) == false)
                {
                    if (fences.Any(n => n.list.Any(f => f.X == area[i].X && f.Y == area[i].Y - 1) && n.side == (0, -1)) == false)
                        fences.Add(AddSide(area[i], area, 0, -1));
                }
                if (area.Any(n => n.X == area[i].X + 1 && n.Y == area[i].Y) == false)
                {
                    if (fences.Any(n => n.list.Any(f => f.X == area[i].X + 1 && f.Y == area[i].Y) && n.side == (1, 0)) == false)
                        fences.Add(AddSide(area[i], area, 1, 0));
                }
                if (area.Any(n => n.X == area[i].X - 1 && n.Y == area[i].Y) == false)
                {
                    if (fences.Any(n => n.list.Any(f => f.X == area[i].X - 1 && f.Y == area[i].Y) && n.side == (-1, 0)) == false)
                        fences.Add(AddSide(area[i], area, -1, 0));
                }
            }
            return fences.Count * area.Count;
        }

        private (List<(int, int)>, (int, int)) AddSide((char, int X, int Y) place, List<(char, int X, int Y)> area, int X, int Y)
        {

            List<(int, int)> newFence = new() { (place.X + X, place.Y + Y) };
            if (Y != 0)
            {
                for (int i = place.X + 1; ; i++)
                {
                    if (area.Any(n => n.X == i && n.Y == place.Y) == false)
                        break;
                    if (area.Any(n => n.X == i && n.Y == place.Y + Y) == false)
                        newFence.Add((i, place.Y + Y));
                    else break;
                }
                for (int i = place.X - 1; ; i--)
                {
                    if (area.Any(n => n.X == i && n.Y == place.Y) == false)
                        break;
                    if (area.Any(n => n.X == i && n.Y == place.Y + Y) == false)
                        newFence.Add((i, place.Y + Y));
                    else break;
                }
            }
            else
            {
                for (int i = place.Y + 1; ; i++)
                {
                    if (area.Any(n => n.X == place.X && n.Y == i) == false)
                        break;
                    if (area.Any(n => n.X == place.X + X && n.Y == i) == false)
                        newFence.Add((place.X + X, i));
                    else break;
                }
                for (int i = place.Y - 1; ; i--)
                {
                    if (area.Any(n => n.X == place.X && n.Y == i) == false)
                        break;
                    if (area.Any(n => n.X == place.X + X && n.Y == i) == false)
                        newFence.Add((place.X + X, i));
                    else break;
                }
            }
            return (newFence, (X, Y));
        }
    }
}
