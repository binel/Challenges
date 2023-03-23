using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day06 : BaseDay
    {
        public override int DayNumber { get; set; } = 6;

        public override string PuzzlePart1(bool training)
        {
            return MainLogic(training, 4);
        }

        public override string PuzzlePart2(bool training)
        {
            return MainLogic(training, 14);
        }

        private string MainLogic(bool training, int length)
        {
            var lines = GetLinesOfInput(training);
            var datastream = lines[0];

            int markerPosition = 0;
            for (int i = 0; i < datastream.Length - length; i++)
            {
                if (IsUniqueCharSubstring(datastream, i, length))
                {
                    markerPosition = i + length;
                    break;
                }
            }

            return markerPosition.ToString();
        }

        private bool IsUniqueCharSubstring(string databuffer, int start, int length)
        {
            var substring = databuffer.Substring(start, length);

            List<char> chars = new List<char>();
            foreach (char c in substring)
            {
                if (chars.Contains(c))
                {
                    return false;
                }
                else {
                    chars.Add(c);
                }
            }

            return true;
        }
    }
}
