using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    internal class Day09Tests : BaseDayTests
    {
        internal override BaseDay Day => new Day09();

        internal override string Part1TrainingSolution => "13";

        internal override string Part1Solution => "5735";

        internal override string Part2TrainingSolution => "1";

        internal override string Part2Solution => "2478";

        [Test]
        public void AdditionalSample()
        {
            Day09 testday = new Day09();

            string[] input = new string[8];
            input[0] = "R 5";
            input[1] = "U 8";
            input[2] = "L 8";
            input[3] = "D 3";
            input[4] = "R 17";
            input[5] = "D 10";
            input[6] = "L 25";
            input[7] = "U 20";

            testday.InputOverride = input;
            Assert.That(testday.InputOverride, Is.Not.Null);
            Assert.That(testday.PuzzlePart2(false), Is.EqualTo("36"));
        }
    }
}
