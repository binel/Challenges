using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    // https://adventofcode.com/2022/day/1

    public class Day01 : BaseDay
    {
        public override int DayNumber { get; set; } = 1;

        private class Elf
        {
            public int Calories { get; set; }
        }

        public override void PuzzlePart1(bool training)
        {
            string[] lines = GetLinesOfInput(training);

            List<Elf> elves = new List<Elf>();
            Elf workingElf = new Elf();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elves.Add(workingElf);
                    workingElf = new Elf();
                }
                else
                {
                    workingElf.Calories += int.Parse(line);
                }
            }

            var sortedElves = elves.OrderByDescending(e => e.Calories).ToList();
            Console.WriteLine($"{sortedElves[0].Calories}");
        }

        public override void PuzzlePart2(bool training)
        {
            string[] lines = GetLinesOfInput(training);

            List<Elf> elves = new List<Elf>();
            Elf workingElf = new Elf();
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    elves.Add(workingElf);
                    workingElf = new Elf();
                }
                else
                {
                    workingElf.Calories += int.Parse(line);
                }
            }

            var sortedElves = elves.OrderByDescending(e => e.Calories).ToList();
            var top3Sum = sortedElves.Take(3).Sum(e => e.Calories);
            Console.WriteLine($"{top3Sum}");
        }
    }
}
