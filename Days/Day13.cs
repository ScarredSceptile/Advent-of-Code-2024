namespace Advent_of_Coding_2024.Days
{
    internal class Day13 : Day
    {
        public void Star1()
        {
            var machines = GetMachines();
            Console.WriteLine(machines.Sum(TokensToWin));
        }

        public void Star2()
        {
            var machines = GetMachines(10000000000000);
            Console.WriteLine(machines.Sum(TokensToWin));
        }

        private List<((long, long), (long, long), (long, long))> GetMachines(long addition = 0)
        {
            var input = Input.GetSingle("Day13").Split("\r\n\r\n");
            List<((long, long), (long, long), (long, long))> machines = new();
            foreach (var block in input)
            {
                var lines = block.Split("\r\n");
                var aVals = lines[0].Split(": ")[1].Split(", ");
                var bVals = lines[1].Split(": ")[1].Split(", ");
                var priceVals = lines[2].Split(": ")[1].Split(", ");
                var a = (long.Parse(aVals[0].Split('+')[1]), long.Parse(aVals[1].Split('+')[1]));
                var b = (long.Parse(bVals[0].Split('+')[1]), long.Parse(bVals[1].Split('+')[1]));
                var price = (long.Parse(priceVals[0].Split('=')[1]) + addition, long.Parse(priceVals[1].Split('=')[1]) + addition);
                machines.Add((a, b, price));
            }
            return machines;
        }

        private long TokensToWin(((long X, long Y) A, (long X, long Y) B, (long X, long Y) Price) machine)
        {
            if (machine.A.X * machine.B.Y - machine.B.X * machine.A.Y == 0)
                return 0;
            var x = (machine.Price.X * machine.B.Y - machine.B.X * machine.Price.Y)/(double)(machine.A.X * machine.B.Y - machine.B.X * machine.A.Y);
            var y = (machine.A.X * machine.Price.Y - machine.Price.X * machine.A.Y)/(double)(machine.A.X * machine.B.Y - machine.B.X * machine.A.Y);
            if (Math.Abs(x-(long)x) < double.Epsilon && Math.Abs(y-(long)y) < double.Epsilon)
                return (long)x * 3 + (long)y;
            return 0;
        }
    }
}
