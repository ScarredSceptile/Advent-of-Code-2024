namespace Advent_of_Coding_2024.Days
{
    internal class Day13 : Day
    {
        public void Star1()
        {
            Solve();
        }

        public void Star2()
        {
            Solve(10000000000000);
        }

        private void Solve(long addition = 0)
        {
            var input = Input.GetSingle("Day13").Split("\r\n\r\n");
            long total = 0;
            foreach (var block in input)
            {
                var lines = block.Split("\r\n");
                var aVals = lines[0].Split(": ")[1].Split(", ");
                var bVals = lines[1].Split(": ")[1].Split(", ");
                var priceVals = lines[2].Split(": ")[1].Split(", ");
                (long X, long Y) a = (long.Parse(aVals[0].Split('+')[1]), long.Parse(aVals[1].Split('+')[1]));
                (long X, long Y) b = (long.Parse(bVals[0].Split('+')[1]), long.Parse(bVals[1].Split('+')[1]));
                (long X, long Y) price = (long.Parse(priceVals[0].Split('=')[1]) + addition, long.Parse(priceVals[1].Split('=')[1]) + addition);

                var x = (price.X * b.Y - b.X * price.Y)/(double)(a.X * b.Y - b.X * a.Y);
                var y = (a.X * price.Y - price.X * a.Y)/(double)(a.X * b.Y - b.X * a.Y);
                if (Math.Abs(x-(long)x) < double.Epsilon && Math.Abs(y-(long)y) < double.Epsilon)
                    total += (long)x * 3 + (long)y;
            }
            Console.WriteLine(total);
        }
    }
}
