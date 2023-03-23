using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    [TestFixture]
    internal abstract class BaseDayTests
    {
        internal abstract BaseDay Day { get; }

        internal abstract string Part1TrainingSolution { get; }

        internal abstract string Part1Solution { get; }

        internal abstract string Part2TrainingSolution { get; }

        internal abstract string Part2Solution { get; }

        [Test]
        public void Part1Training()
        {
            Assert.That(Day.PuzzlePart1(true), Is.EqualTo(Part1TrainingSolution));
        }

        [Test]
        public void Part1()
        {
            Assert.That(Day.PuzzlePart1(false), Is.EqualTo(Part1Solution));
        }

        [Test]
        public void Part2Training()
        {
            Assert.That(Day.PuzzlePart2(true), Is.EqualTo(Part2TrainingSolution));
        }

        [Test]
        public void Part2()
        {
            Assert.That(Day.PuzzlePart2(false), Is.EqualTo(Part2Solution));
        }
    }
}
