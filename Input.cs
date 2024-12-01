using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024
{
    internal static class Input
    {
        public static string[] Get(string file)
        {
            var dir = Directory.GetCurrentDirectory();
            var measurements = File.ReadAllLines(dir.Substring(0, dir.IndexOf("bin")) + @"\Input\" + file + ".txt");
            return measurements;
        }

        public static string GetSingle(string file)
        {
            var dir = Directory.GetCurrentDirectory();
            var measurements = File.ReadAllText(dir.Substring(0, dir.IndexOf("bin")) + @"\Input\" + file + ".txt");
            return measurements;
        }
    }
}
