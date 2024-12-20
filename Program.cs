using Advent_of_Coding_2024.Days;
using System.Diagnostics;

namespace Advent_of_Coding_2024
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var time = new Stopwatch();
            time.Start();
            var day = new Day20();
            day.Star2();
            time.Stop();
            Console.WriteLine(time.Elapsed);
        }
    }
}