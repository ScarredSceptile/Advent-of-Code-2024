using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day4 : Day
    {
        public void Star1()
        {
            var input = Input.Get("Day4");
            var search = input.ToList();
            search.AddRange(input.Select(n => Reverse(n)));
            string next, diagtr, diagtl, diagbr, diagbl;
            var length = input.Length;
            for (int i = 0; i < length; i++)
            {
                next = diagtr = diagtl = diagbr = diagbl = "";
                for (int j = 0; j < length; j++)
                {
                    next += input[j][i];
                }
                for (int j = 0; j <= i; j++)
                {
                    diagtl += input[i - j][j];
                    diagbr += input[length - 1 - (i - j)][length - 1 - j];
                    diagtr += input[length - 1 - (i - j)][j];
                    diagbl += input[i - j][length - 1 - j];
                }
                search.Add(next);
                search.Add(Reverse(next));
                search.Add(diagtr);
                search.Add(diagtl);
                search.Add(Reverse(diagtr));
                search.Add(Reverse(diagtl));
                if (i != length - 1)
                {
                    search.Add(diagbr);
                    search.Add(diagbl);
                    search.Add(Reverse(diagbr));
                    search.Add(Reverse(diagbl));
                }
            }
            Console.WriteLine(search.Sum(n => Regex.Matches(n, "XMAS").Count));
        }

        public void Star2()
        {
            var input = Input.Get("Day4");
            int crosses = 0;
            for (int i = 0; i < input.Length - 2; i++)
            {
                for (int j = 0; j < input.Length - 2; j++)
                {
                    var cube = input[i..(i+3)].Select(n => n.Substring(j, 3)).ToArray();
                    if (cube[1][1] != 'A' || cube[0][0] == cube[2][2])
                        continue;
                    var corners = new[] { cube[0][0], cube[0][2], cube[2][0], cube[2][2] };
                    if (corners.Where(n => n == 'M').Count() == 2 && corners.Where(n => n == 'S').Count() == 2)
                        crosses++;
                }
            }
            Console.WriteLine(crosses);
        }

        private string Reverse(string text)
        {
            var arr = text.ToArray();
            Array.Reverse(arr);
            return new string(arr);
        }
    }
}
