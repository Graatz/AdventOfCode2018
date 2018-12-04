using AdventOfCode.Tasks;
using System;

namespace AdventOfCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Day1 day1 = new Day1("Inputs/day1.txt");
            Console.WriteLine("Day 1 Part 1: " + day1.Part1());
            Console.WriteLine("Day 1 Part 2: " + day1.Part2());
            Console.ReadLine();

            Day2 day2 = new Day2("Inputs/day2.txt");
            Console.WriteLine("Day 2 Part 1: " + day2.Part1());
            Console.WriteLine("Day 2 Part 2: " + day2.Part2());
            Console.ReadLine();

            Day3 day3 = new Day3("Inputs/day3.txt");
            Console.WriteLine("Day 3 Part 1: " + day3.Part1());
            Console.WriteLine("Day 3 Part 2: " + day3.Part2());
            Console.ReadLine();
        }
    }
}
