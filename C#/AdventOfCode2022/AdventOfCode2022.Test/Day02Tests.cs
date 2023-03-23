using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022.Test
{
    [TestFixture]
    internal class Day02Tests : BaseDayTests
    {
        internal override BaseDay Day => new Day02();

        internal override string Part1TrainingSolution => "15";

        internal override string Part1Solution => "13526";

        internal override string Part2TrainingSolution => "12";

        internal override string Part2Solution => "14204";
    }
}
