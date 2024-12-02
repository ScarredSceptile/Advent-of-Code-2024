using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Markup;

namespace Advent_of_Coding_2024.Days
{
    internal class Day2 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day2");
            Console.WriteLine(input.Count(n => IsSafe(n.Split(' ').Select(int.Parse).ToArray())));
        }

        public void Star2()
        {
            var input = Input.Get("Day2");
            Console.WriteLine(input.Count(n => CanBeSafe(n)));
        }

        private bool IsSafe(int[] values)
        {
            bool increase = values[0] < values[1];
            for (int i = 1; i < values.Length; i++)
            {
                int diff;
                if (increase)
                    diff = values[i] - values[i - 1];
                else
                    diff = values[i - 1] - values[i];

                if (diff < 1 || diff > 3)
                {
                    return false;
                }
            }
            return true;
        }

        private bool CanBeSafe(string line)
        {
            var values = line.Split(' ').Select(int.Parse).ToArray();
            if (IsSafe(values))
                return true;
            for (int i = 0; i < values.Length; i++)
            {
                var temp = values.ToList();
                temp.RemoveAt(i);
                if (IsSafe(temp.ToArray()))
                    return true;
            }
            return false;
        }
    }
}