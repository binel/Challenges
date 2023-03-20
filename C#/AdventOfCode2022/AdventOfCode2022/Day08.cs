using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using static AdventOfCode2022.Day08;


namespace AdventOfCode2022
{
    /*
     * Improvements: 
     * - Reaching in too far into Forrest, maybe make an iterator
     * 
     */

    public class Day08 : BaseDay
    {
        public override int DayNumber { get; set; } = 8;

        public override void PuzzlePart1(bool training)
        {
            string[] lines = GetLinesOfInput(training);

            Forrest forrest = new Forrest(lines);
            int count = 0;
            for (int h = 0; h < forrest.Height; h++)
            {
                for (int w = 0; w < forrest.Width; w++)
                {
                    count += forrest.IsTreeVisible(w, h) ? 1 : 0;
                }
            }
            Console.WriteLine(count);
        }

        public override void PuzzlePart2(bool training)
        {
            Console.WriteLine("Not done");
        }


        public class Tree
        {
            public int Height { get; set; }
    
            public Tree(int height)
            {
                Height = height;
            }
        }

        public class Forrest
        {
            public Tree[,] TreeGrid { get; set; }

            public int Width { get; set; }

            public int Height { get; set; }


            public Forrest(string[] input)
            {
                Width = input[0].Length;
                Height = input.Length;
            
                TreeGrid = new Tree[Width, Height];

                for (int h = 0; h < Height; h++)
                {
                    var line = input[h];
                    for (int w = 0; w < Width; w++)
                    {
                        TreeGrid[w, h] = new Tree((int)char.GetNumericValue(line[w]));
                    }
                }
            }

            public bool IsTreeVisible(int x, int y)
            {
                Tree t = TreeGrid[x,y];

                if (x == 0 || x == Width || y == 0 || y == Height)
                {
                    return true;
                }

                bool hidden = true;

                hidden &= !CheckVisibleBySegment(t, 0, x, true, y);
                hidden &= !CheckVisibleBySegment(t, x + 1, Width, true, y);
                hidden &= !CheckVisibleBySegment(t, 0, y, false, x);
                hidden &= !CheckVisibleBySegment(t, y + 1, Height, false, x);

                return !hidden;
            }

            private bool CheckVisibleBySegment(Tree t, int start, int end, bool row, int invarient)
            {
                if (row)
                {
                    for (int i = start; i < end; i++)
                    {
                        if (TreeGrid[i, invarient].Height >= t.Height)
                        {
                            return false;
                        }
                    }
                    return true;
                }
                for (int i = start; i < end; i++)
                {
                    if (TreeGrid[invarient, i].Height >= t.Height)
                    {
                        return false;
                    }
                }
                return true;


            }
        }
    }
}
