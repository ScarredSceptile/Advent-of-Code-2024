using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day7 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day7").Select(n => n.Split(": ")).ToArray();
            Console.WriteLine(input.Where(n => CanBePossible(long.Parse(n[0]), n[1].Split(" ").Select(long.Parse).ToArray())).Sum(n => long.Parse(n[0])));
        }

        public void Star2()
        {
            var input = Input.Get("Day7").Select(n => n.Split(": ")).ToArray();
            Console.WriteLine(input.Where(n => CanBePossibleConcat(long.Parse(n[0]), n[1].Split(" ").Select(long.Parse).ToArray())).Sum(n => long.Parse(n[0])));
        }

        private bool CanBePossible(long value, long[] values, long curValue = 0, int index = 0)
        {
            if (index == values.Length)
                return curValue == value;
            else
            {
                return CanBePossible(value, values, curValue * values[index], index + 1) || CanBePossible(value, values, curValue + values[index], ++index);
            }

        }

        private bool CanBePossibleConcat(long value, long[] values, long curValue = 0, int index = 0)
        {

            if (index == values.Length || curValue > value)
                return curValue == value;
            else
            {
                bool simplePossible = CanBePossibleConcat(value, values, curValue * values[index], index + 1) || CanBePossibleConcat(value, values, curValue + values[index], index + 1);
                return simplePossible || CanBePossibleConcat(value, values, long.Parse(curValue.ToString() + values[index].ToString()), ++index);
            }
        }
    }
}
