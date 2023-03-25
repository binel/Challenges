using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    internal class Day04Tests : BaseDayTests
    {
        internal override BaseDay Day => new Day04();

        internal override string Part1TrainingSolution => "2";

        internal override string Part1Solution => "588";

        internal override string Part2TrainingSolution => "4";

        internal override string Part2Solution => "911";

        [TestCase("1-1", "2-2", ExpectedResult = false)]
        [TestCase("1-2", "2-2", ExpectedResult = true)]
        [TestCase("1-5", "2-2", ExpectedResult = true)]
        [TestCase("1-1", "1-2", ExpectedResult = true)]
        public bool Overlap(string i1, string i2)
        {
            Day04.Section s1 = new Day04.Section(i1);
            Day04.Section s2 = new Day04.Section(i2);

            return s1.Overlap(s2);
        }
    }
}
