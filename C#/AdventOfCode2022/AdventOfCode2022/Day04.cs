using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day04 : BaseDay
    {
        public override int DayNumber { get; set; } = 4;

        public override string PuzzlePart1(bool training)
        {
            Func<Section, Section, bool> countCondition = (s1, s2) => s1.Contains(s2) || s2.Contains(s1);

            return CommonLogic(countCondition, training);
        }

        public override string PuzzlePart2(bool training)
        {
            Func<Section, Section, bool> countCondition = (s1, s2) => s1.Overlap(s2);

            return CommonLogic(countCondition, training);
        }

        private string CommonLogic(Func<Section, Section, bool> countCondition, bool training)
        {
            string[] lines = GetLinesOfInput(training);

            int count = 0;
            foreach (var line in lines)
            {
                var split = line.Split(",");
                Section s1 = new Section(split[0]);
                Section s2 = new Section(split[1]);

                if (countCondition(s1, s2))
                {
                    count += 1;
                }
            }
            return count.ToString();
        }

        private class Section
        {
            public int Start { get; set; }
            public int End { get; set; }
            public int Length => End - Start + 1;

            public Section(string section)
            {
                var split = section.Split("-");
                Start = int.Parse(split[0]);
                End = int.Parse(split[1]);
            }

            public bool Contains(Section s)
            {
                return s.Start >= Start && s.End <= End;
            }

            public bool Contains(int i)
            {
                return i >= Start && i <= End;
            }

            public bool Overlap(Section s)
            {
                for (int i = Start; i <= End; i++)
                {
                    if (s.Contains(i))
                    {
                        return true;
                    }
                }
                return false;
            }
        }
    }
}
