using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
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

        public override string PuzzlePart1(bool training)
        {
            string[] lines = GetInput(training);

            Forrest forrest = new Forrest(lines);
            int count = 0;
            for (int h = 0; h < forrest.Height; h++)
            {
                for (int w = 0; w < forrest.Width; w++)
                {
                    count += forrest.IsTreeVisible(w, h) ? 1 : 0;
                }
            }
            return count.ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            string[] lines = GetInput(training);

            Forrest forrest = new Forrest(lines);
            int maxScenicScore = 0;
            for (int h = 0; h < forrest.Height; h++)
            {
                for (int w = 0; w < forrest.Width; w++)
                {
                    int score = forrest.ScenicScore(w, h);
                    maxScenicScore = Math.Max(score, maxScenicScore);
                }
            }
            return maxScenicScore.ToString();
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

            public int ScenicScore(int x, int y)
            {
                if (x == 0 || y == 0 || x == Width - 1 || y == Height - 1)
                {
                    return 0;
                }

                int score;
                int rowScore = 0;
                for (int i = x - 1; i >= 0; i--)
                {
                    rowScore++;
                    if (TreeGrid[i, y].Height >= TreeGrid[x, y].Height)
                    {
                        break;
                    }
                }
                score = rowScore;
                rowScore = 0;

                for (int i = x + 1; i < Width; i++)
                {
                    rowScore++;
                    if (TreeGrid[i, y].Height >= TreeGrid[x, y].Height)
                    {
                        break;
                    }
                }
                score *= rowScore;
                rowScore = 0;

                for (int i = y - 1; i >= 0; i--)
                {
                    rowScore++;
                    if (TreeGrid[x, i].Height >= TreeGrid[x, y].Height)
                    {
                        break;
                    }
                }
                score *= rowScore;
                rowScore = 0;

                for (int i = y + 1; i < Height; i++)
                {
                    rowScore++;
                    if (TreeGrid[x, i].Height >= TreeGrid[x, y].Height)
                    {
                        break;
                    }
                }
                score *= rowScore;
                return score;
            }

            public int ViewingDistance(Tree t, int start, int end, bool row, int invarient)
            {
                int visibleTrees = 0;
                if (row)
                {
                    for (int i = start; i < end; i++)
                    {
                        visibleTrees++;
                        if (TreeGrid[i, invarient].Height > t.Height)
                        {
                            return visibleTrees;
                        }
                    }
                }
                for (int i = start; i < end; i++)
                {
                    visibleTrees++;
                    if (TreeGrid[invarient, i].Height > t.Height)
                    {
                        return visibleTrees;
                    }
                }

                return visibleTrees;
            }
        }
    }
}
