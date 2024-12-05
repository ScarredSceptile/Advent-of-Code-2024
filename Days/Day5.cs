namespace Advent_of_Coding_2024.Days
{
    internal class Day5 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day5").Split("\r\n\r\n");
            var orders = input[0].Split("\r\n").Select(n => n.Split("|")).ToArray();
            var updates = input[1].Split("\r\n");
            int middlePages = 0;
            foreach (var update in updates)
            {
                var nums = update.Split(",");
                List<string> updated = new();
                bool safe = true;
                foreach (var item in nums)
                {
                    if (orders.Where(n => n[0] == item).Any(n => updated.Contains(n[1])))
                    {
                        safe = false; break;
                    }
                    updated.Add(item);
                }
                if (safe)
                {
                    middlePages += int.Parse(nums[nums.Length/2]);
                }
            }
            Console.WriteLine(middlePages);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day5").Split("\r\n\r\n");
            var orders = input[0].Split("\r\n").Select(n => n.Split("|")).ToArray();
            var updates = input[1].Split("\r\n");
            int middlePages = 0;
            foreach (var update in updates)
            {
                var nums = update.Split(",");
                List<string> updated = new();
                foreach (var item in nums)
                {
                    if (orders.Where(n => n[0] == item).Any(n => updated.Contains(n[1])))
                    {
                        var ordered = OrderInvalid(nums, orders);
                        middlePages += int.Parse(ordered[ordered.Length/2]);
                        break;
                    }
                    updated.Add(item);
                }
            }
            Console.WriteLine(middlePages);
        }

        private string[] OrderInvalid(string[] nums, string[][] orders)
        {
            List<string> ordered = new();
            for (int i = 0; i < nums.Length; i++)
            {
                if (orders.Where(n => n[0] == nums[i]).Any(n => ordered.Contains(n[1])))
                {
                    var place = orders.Where(n => n[0] == nums[i]).Where(n => ordered.Contains(n[1]));
                    int pos = ordered.IndexOf(ordered.First(n => place.Any(c => n == c[1])));
                    ordered.Insert(pos, nums[i]);
                }
                else
                {
                    ordered.Add(nums[i]);
                }
            }
            return ordered.ToArray();
        }
    }
}
