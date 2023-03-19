using System.Diagnostics;

namespace AdventOfCode2022
{
    public class Program
    {
        static void Main(string[] args)
        {
            // Day01.FindWellPackedElf();
            Stopwatch s = Stopwatch.StartNew();
            Day02.FindTotalScore_Part2();
            Console.WriteLine($"{s.Elapsed}");
        }
    }
}