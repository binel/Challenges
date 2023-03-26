using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day12 : BaseDay
    {
        public override int DayNumber { get; set; } = 12;

        private Display _d;
        private int _width;
        private int _height;
        private char[,] _grid;

        public override string PuzzlePart1(bool training)
        {
            var input = GetInput(training);
            _width = input[0].Length;
            _height = input.Length;
            _grid = new char[_height, _width];
            _d = new Display(_width, _height);
            for (int y = 0; y < _height; y++)
            {
                var line = input[y];
                for (int x = 0; x < _width; x++)
                {
                    _grid[y, x] = line[x];
                }
            }
            DisplayGrid();
            Console.ReadLine();
            return "1";
        }

        public override string PuzzlePart2(bool training)
        {
            return "Not Done";
        }

        private void DisplayGrid()
        {
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    _d.SetChar(x, y, _grid[y, x]);
                }
            }
            _d.Draw();
        }
    }
}
