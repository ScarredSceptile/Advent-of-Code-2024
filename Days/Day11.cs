namespace Advent_of_Coding_2024.Days
{
    internal class Day11 : Day
    {
        public void Star1()
        {
            Console.WriteLine(SplitStone(25));
        }

        public void Star2()
        {
            Console.WriteLine(SplitStone(75));
        }

        private long SplitStone(int max)
        {
            var input = Input.GetSingle("Day11").Split(' ').ToList();
            Dictionary<string, string[]> stoneMap = new() { ["0"] = new[] { "1" } };
            Dictionary<string, long> values = input.ToDictionary(n => n, n => (long)1);
            for (int i = 0; i < max; i++)
            {
                Dictionary<string, long> newValues = new();
                foreach (var value in values)
                {
                    if (stoneMap.ContainsKey(value.Key))
                        foreach (var val in stoneMap[value.Key])
                        {
                            if (!newValues.ContainsKey(val))
                                newValues.Add(val, 0);
                            newValues[val] += value.Value;
                        }
                    else
                    {
                        if (value.Key.Length % 2 == 0)
                        {
                            var item1Value = long.Parse(value.Key[..(value.Key.Length / 2)]);
                            var item2Value = long.Parse(value.Key[(value.Key.Length / 2)..]);
                            var item1 = item1Value.ToString();
                            var item2 = item2Value.ToString();

                            if (!newValues.ContainsKey(item1))
                                newValues.Add(item1, 0);
                            if (!newValues.ContainsKey(item2))
                                newValues.Add(item2, 0);
                            stoneMap.Add(value.Key, new[] { item1, item2 });
                            newValues[item1] += value.Value;
                            newValues[item2] += value.Value;
                        }
                        else
                        {
                            var newkeylong = (long.Parse(value.Key) * 2024);
                            var newkey = newkeylong.ToString();
                            if (!newValues.ContainsKey(newkey))
                                newValues.Add(newkey, 0);
                            stoneMap.Add(value.Key, new[] { newkey });
                            newValues[newkey] += value.Value;
                        }
                    }
                }
                values = newValues;
            }
            return values.Sum(n => n.Value);
        }
    }
}
