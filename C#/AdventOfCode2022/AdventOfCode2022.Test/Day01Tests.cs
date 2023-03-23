using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    [TestFixture]
    internal class Day01Tests: BaseDayTests
    {
        internal override BaseDay Day => new Day01();

        internal override string Part1TrainingSolution => "24000";

        internal override string Part1Solution => "67450";

        internal override string Part2TrainingSolution => "45000";

        internal override string Part2Solution => "199357";
    }
}
