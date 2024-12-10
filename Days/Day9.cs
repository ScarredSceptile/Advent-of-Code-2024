using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day9 : Day
    {
        public void Star1()
        {
            var list = GetFileSystem();
            int lastID = list.Count - 1;
            int firstFree = 0;
            while (lastID > firstFree)
            {
                while (list[lastID] == "." && lastID != firstFree)
                    lastID--;
                while (list[firstFree] != "."  && lastID != firstFree)
                    firstFree++;
                if (lastID == firstFree) { break; }
                list[firstFree] = list[lastID];
                list[lastID] = ".";
            }
            long mult = 0;
            Console.WriteLine(list.Where(n => n != ".").Sum(n => mult++ * long.Parse(n)));
        }

        public void Star2()
        {
            var list = GetFileSystem();
            int lastID = list.Count - 1;
            while (lastID >= 0)
            {
                while (list[lastID] == ".")
                    lastID--;
                var ID = list[lastID];
                var count = list.Where(n => n == ID).Count();
                int freeSpace = 0;
                while (freeSpace < lastID)
                {
                    if (list[freeSpace] != ".")
                    {
                        freeSpace++;
                    }
                    else
                    {
                        int freeSpaceStart = freeSpace;
                        int freeCount = 1;
                        while (list[++freeSpace] == ".")
                            freeCount++;
                        if (freeCount >= count)
                        {
                            for (int i = 0; i < count; i++)
                                list[lastID - i] = ".";
                            for (int i = freeSpaceStart; i < freeSpaceStart + count; i++)
                            {
                                list[i] = ID;
                            }
                            lastID -= count;
                            break;
                        }
                    }
                }
                if (list[lastID] == ID)
                {
                    lastID -= count;
                }
            }
            long sum = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (list[i] != ".")
                {
                    sum += long.Parse(list[i]) * i;
                }
            }
            Console.WriteLine(sum);
        }

        private List<string> GetFileSystem()
        {
            var input = Input.GetSingle("Day9");
            List<string> list = new List<string>();
            int curID = 0;
            bool free = false;
            foreach (var item in input)
            {
                if (free)
                {
                    free = false;
                    list.AddRange(Enumerable.Repeat(".", int.Parse(item.ToString())));
                }
                else
                {
                    free = true;
                    list.AddRange(Enumerable.Repeat(curID.ToString(), int.Parse(item.ToString())));
                    curID++;
                }
            }
            return list;
        }
    }
}
