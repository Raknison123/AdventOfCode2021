using AdventOfCode2021.Solutions;
using System;

namespace AdventOfCode2021
{
    class Program
    {
        static void Main(string[] args)
        {
            var puzzle = new Day20();
            Console.WriteLine($"{puzzle.GetType().Name} - Part1:{puzzle.Part1}, Part2:{puzzle.Part2}");
            Console.ReadKey();
        }
    }
}
