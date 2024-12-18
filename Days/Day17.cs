using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Advent_of_Coding_2024.Days
{
    internal class Day17 : Day
    {
        public void Star1()
        {
            var input = Input.GetSingle("Day17").Split("\r\n\r\n");
            var registers = input[0].Split("\r\n").Select(n => n.Split(": ")[1]).Select(long.Parse).ToArray();
            var program = input[1].Split(": ")[1].Split(',').Select(int.Parse).ToArray(); List<long> output = new();
            //registers[0] = 262144;
            Console.WriteLine(GetOutput(registers, program));
        }

        public void Star2()
        {
            var input = Input.GetSingle("test").Split("\r\n\r\n");
            var registers = input[0].Split("\r\n").Select(n => n.Split(": ")[1]).Select(long.Parse).ToArray();
            var program = input[1].Split(": ")[1].Split(',').Select(int.Parse).ToArray();
            registers[0] = 0;
            for (int i = program.Length - 1; i >= 0; i--)
            {
                ReverseOutput(registers, program[..^2], program[i]);
            }
            Console.WriteLine(registers[0]);
        }

        private string GetOutput(long[] registers, int[] program)
        {
            List<int> output = new();
            for (int i = 0; i < program.Length;)
            {
                switch (program[i])
                {
                    case 0:
                        registers[0] /= (long)Math.Pow(2, GetOperandValue(program[i + 1], registers));
                        i += 2;
                        break;
                    case 1:
                        registers[1] = registers[1] ^ program[i + 1];
                        i += 2;
                        break;
                    case 2:
                        registers[1]  = GetOperandValue(program[i + 1], registers) % 8;
                        i += 2;
                        break;
                    case 3:
                        if (registers[0] != 0)
                            i = program[i + 1] - 2;
                        i += 2;
                        break;
                    case 4:
                        registers[1] = registers[1] ^ registers[2];
                        i += 2;
                        break;
                    case 5:
                        output.Add((int)GetOperandValue(program[i + 1], registers) % 8);
                        i += 2;
                        break;
                    case 6:
                        registers[1] = registers[0] / (long)Math.Pow(2, GetOperandValue(program[i + 1], registers));
                        i += 2;
                        break;
                    case 7:
                        registers[2] = registers[0] / (long)Math.Pow(2, GetOperandValue(program[i + 1], registers));
                        i += 2;
                        break;
                    default: break;
                }
            }
            return string.Join(",", output);
        }

        private void ReverseOutput(long[] registers, int[] program, int value)
        {
            List<int> output = new();
            for (int i = program.Length - 2; i >= 0; i-= 2)
            {
                switch (program[i])
                {
                    case 0:
                        var increase = (long)Math.Pow(2, GetOperandValue(program[i + 1], registers));
                        if (registers[0] == 0)
                            registers[0] = increase;
                        else registers[0] *= increase;
                        break;
                    case 1:
                        registers[1] = registers[1] ^ program[i + 1];
                        break;
                    case 2:
                        registers[0]  += (registers[1] - registers[0]) % 8;
                        break;
                    case 4:
                        registers[1] = registers[1] ^ registers[2];
                        break;
                    case 5:
                        registers[1] = value;
                        break;
                    case 6:
                        registers[0] += registers[0] - (registers[1] * (long)Math.Pow(2, GetOperandValue(program[i + 1], registers)));
                        break;
                    case 7:
                        registers[0] += registers[0] - (registers[2] * (long)Math.Pow(2, GetOperandValue(program[i + 1], registers)));
                        break;
                    default: break;
                }
            }
        }

        private long GetOperandValue(int operand, long[] registers)
        {
            if (operand >= 0 && operand <= 3)
                return operand;
            if (operand == 4)
                return registers[0];
            if (operand == 5)
                return registers[1];
            if (operand == 6)
                return registers[2];
            return -1;
        }
    }
}
