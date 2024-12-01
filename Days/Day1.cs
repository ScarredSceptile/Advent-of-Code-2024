namespace Advent_of_Coding_2024.Days
{
    internal class Day1 : Day
    {
        public void Star1()
        {
            var (left, right) = GetLists();
            var zipped = left.OrderBy(n => n).Zip(right.OrderBy(n => n));
            Console.WriteLine(zipped.Sum(n => Math.Abs(n.First - n.Second)));
        }

        public void Star2()
        {
            var (left, right) = GetLists();
            Console.WriteLine(left.Sum(n => right.Count(c => c == n) * n));
        }

        private static (List<int> left, List<int> right) GetLists()
        {
            var input = Input.Get("Day1");
            List<int> leftList = new();
            List<int> rightList = new();

            foreach (var item in input)
            {
                var lr = item.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                leftList.Add(int.Parse(lr[0]));
                rightList.Add(int.Parse(lr[1]));
            }
            return (leftList, rightList);
        }
    }
}
