using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    // https://adventofcode.com/2022/day/1

    public static class Day01
    {
        private const string trainingFilePath = "Input/input_01_0.txt";
        private const string filePath = "Input/input_01_1.txt";

        private class Elf
        {
            public int Calories { get; set; }
        }

        public static void FindWellPackedElf()
        {
            string[] lines = File.ReadAllLines(filePath);

            List<Elf> elves = new List<Elf>();
            Elf workingElf = new Elf();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elves.Add(workingElf);
                    workingElf = new Elf();
                }
                else {
                    workingElf.Calories += int.Parse(line);
                }
            }

            var sortedElves = elves.OrderByDescending(e => e.Calories).ToList();
            Console.WriteLine($"Answer 1: {sortedElves[0].Calories}");

            var top3Sum = sortedElves.Take(3).Sum(e => e.Calories);
            Console.WriteLine($"Answer 2: {top3Sum}");
        }
    }
}
