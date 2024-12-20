using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day19 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day19").Split("\r\n\r\n");
            var patterns = input[0].Split(", ").OrderByDescending(n => n.Length).ToArray();
            var wanted = input[1].Split("\r\n");
            int total = 0;
            foreach (var flag in wanted)
            {
                var parts = patterns.Where(n => flag.Contains(n)).ToArray();
                if (CanBeMade(flag, parts))
                    total++;
                Console.WriteLine(total);
            }
            Console.WriteLine(total);
        }

        public void Star2()
        {
            throw new NotImplementedException();
        }

        private bool CanBeMade(string flag, string[] patterns)
        {
            var matches = patterns.Where(n => flag.StartsWith(n)).ToArray();
            if (matches.Count() == 0) return false;

            foreach (var match in matches)
            {
                var temp = flag;
                temp = temp.Substring(match.Length);
                if (temp.Length == 0) return true;
                var parts = patterns.Where(n => flag.Contains(n)).ToArray();
                if (CanBeMade(temp, parts)) return true;
            }
            return false;
        }
    }
}
