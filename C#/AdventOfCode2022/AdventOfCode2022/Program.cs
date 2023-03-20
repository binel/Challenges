using System.Diagnostics;

namespace AdventOfCode2022
{
    public class Program
    {
        static void Main(string[] args)
        {
            BaseDay day = new Day08();

            Console.WriteLine("Part 1 Training:");
            day.PuzzlePart1(true);
            Console.WriteLine("Part 1 Puzzle:");
            day.PuzzlePart1(false);
            Console.WriteLine("Part 2 Training:");
            day.PuzzlePart2(true);
            Console.WriteLine("Part 2 Puzzle:");
            day.PuzzlePart2(false);
        }
    }
}