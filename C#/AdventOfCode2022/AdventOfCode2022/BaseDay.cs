using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public abstract class BaseDay
    {
        public abstract int DayNumber { get; set; }

        private string _trainingFilePath { get { return $"Input/input_{DayNumber.ToString().PadLeft(2, '0')}_0.txt"; } }
        private string _puzzleInputPath { get { return $"Input/input_{DayNumber.ToString().PadLeft(2, '0')}_1.txt"; } }

        public abstract void PuzzlePart1(bool training);

        public abstract void PuzzlePart2(bool training);

        public string[] GetLinesOfInput(bool training) => File.ReadAllLines(training ? _trainingFilePath : _puzzleInputPath);


    }
}
