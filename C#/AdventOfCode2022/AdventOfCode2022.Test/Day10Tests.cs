using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    internal class Day10Tests : BaseDayTests
    {
        internal override BaseDay Day => new Day10();

        internal override string Part1TrainingSolution => "13140";

        internal override string Part1Solution => "14360";

        internal override string Part2TrainingSolution => "0";

        internal override string Part2Solution => "0";

        [Test]
        public void SmallProgram()
        {
            Day10.CPU cpu = new Day10.CPU();
            string[] program = new string[3];
            program[0] = "noop";
            program[1] = "addx 3";
            program[2] = "addx -5";

            cpu.Program = program;

            cpu.Tick();
            Assert.That(cpu.Register, Is.EqualTo(1));
            cpu.Tick();
            Assert.That(cpu.Register, Is.EqualTo(1));
            cpu.Tick();
            Assert.That(cpu.Register, Is.EqualTo(4));
            cpu.Tick();
            cpu.Tick();
            Assert.That(cpu.Register, Is.EqualTo(-1));
            cpu.Tick();
            Assert.That(cpu.ProgramComplete, Is.True);
        }

        [TestCase(20, ExpectedResult = 420)]
        [TestCase(60, ExpectedResult = 1140)]
        [TestCase(100, ExpectedResult = 1800)]
        [TestCase(140, ExpectedResult = 2940)]
        [TestCase(180, ExpectedResult = 2880)]
        [TestCase(220, ExpectedResult = 3960)]
        public int Part1Training_StrengthAtCycle(int cycle)
        {
            Day10 d = new Day10();
            Day10.CPU cpu = new Day10.CPU();
            cpu.Program = d.GetInput(true);

            while(cpu.Cycle != cycle)
            {
                cpu.Tick();
            }

            return cpu.SignalStrength;
        }
    }
}
