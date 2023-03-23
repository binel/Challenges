using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    [TestFixture]
    public class Day01Tests
    {
        [Test]
        public void Part1Training()
        {
            Day01 day = new();
            Assert.That(day.PuzzlePart1(true), Is.EqualTo("24000"));
        }

        [Test]
        public void Part1()
        {
            Day01 day = new();
            Assert.That(day.PuzzlePart1(false), Is.EqualTo("67450"));
        }

        [Test]
        public void Part2Training()
        {
            Day01 day = new();
            Assert.That(day.PuzzlePart2(true), Is.EqualTo("45000"));
        }

        [Test]
        public void Part2()
        {
            Day01 day = new();
            Assert.That(day.PuzzlePart2(false), Is.EqualTo("199357"));
        }
    }
}
