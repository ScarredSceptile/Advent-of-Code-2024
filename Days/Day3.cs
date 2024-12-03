using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day3 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day3");
            var matches = Regex.Matches(input, "mul\\(\\d*,\\d*\\)");
            long total = 0;
            foreach (Match match in matches)
            {
                var mult = match.Result;
                var tex = mult.Target.ToString();
                tex = tex[4..^1];
                var nums = tex.Split(',').Select(long.Parse).ToArray();
                total += nums[0] * nums[1];
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            var input = Input.GetSingle("Day3").Split("do()");
            long total = 0;
            foreach(var d in input)
            {
                var does = d.Split("don't()")[0];
                var matches = Regex.Matches(does, "mul\\(\\d*,\\d*\\)");

                foreach (Match match in matches)
                {
                    var mult = match.Result;
                    var tex = mult.Target.ToString();
                    tex = tex[4..^1];
                    var nums = tex.Split(',').Select(long.Parse).ToArray();
                    total += nums[0] * nums[1];
                }
            }
            Console.WriteLine(total);
        }
    }
}
