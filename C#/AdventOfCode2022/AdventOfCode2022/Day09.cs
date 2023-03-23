using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day09 : BaseDay
    {
        public override int DayNumber { get; set; } = 9;

        public override string PuzzlePart1(bool training)
        {
            string[] lines = GetLinesOfInput(training);
            Simulation s = new Simulation();
            
            foreach (var line in lines)
            {
                s.ExecuteMove(line);
            }

            s.PrintSimulationState();

            return s.CountUniqueTailPositions().ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            return "Not done";
        }

        public class Simulation
        {
            public List<Position> UniqueTailPositions { get; } = new List<Position>();

            public Knot Head { get; } = new Knot(new Position(0, 0));

            public Knot Tail { get; } = new Knot(new Position(0, 0));

            public int MinXPos { get; set; } = 0;

            public int MaxXPos { get; set; } = 0;

            public int MinYPos { get; set; } = 0;

            public int MaxYPos { get; set; } = 0;

            public void ExecuteMove(string move)
            {
                var split = move.Split(" ");
                var direction = split[0];
                int distance = int.Parse(split[1]);

                for (int i = 0; i < distance; i++)
                {
                    MoveHead(direction);
                    MoveTail();
                    if (UniqueTailPositions.Find(p => p.Equals(Tail.Position)) == null)
                    {
                        UniqueTailPositions.Add(new Position(Tail.Position.XPos, Tail.Position.YPos));
                    }
                    UpdateSimulationBoundaries();
                }
            }

            public void UpdateSimulationBoundaries()
            {
                MinXPos = Math.Min(MinXPos, Math.Min(Head.Position.XPos, Tail.Position.XPos));
                MinYPos = Math.Min(MinYPos, Math.Min(Head.Position.YPos, Tail.Position.YPos));
                MaxXPos = Math.Max(MaxXPos, Math.Max(Head.Position.XPos, Tail.Position.XPos));
                MaxYPos = Math.Max(MaxYPos, Math.Max(Head.Position.YPos, Tail.Position.YPos));
            }

            public void PrintSimulationState()
            {
                for (int y = MaxYPos; y >= MinYPos; y--)
                {
                    string line = "";
                    for (int x = MinXPos; x <= MaxXPos; x++)
                    {
                        Position pos = new Position(x, y);
                        if (Head.Position.Equals(pos))
                        {
                            line += "H";
                        }
                        else if (Tail.Position.Equals(pos))
                        {
                            line += "T";
                        }
                        else if (UniqueTailPositions.Contains(pos))
                        {
                            line += "#";
                        }
                        else
                        {
                            line += ".";
                        }
                    }
                    Console.WriteLine(line);
                }
            }

            public void MoveHead(string direction)
            {
                switch(direction)
                {
                    case "U":
                        Head.Position.YPos += 1;
                        break;
                    case "D":
                        Head.Position.YPos -= 1;
                        break;
                    case "R":
                        Head.Position.XPos += 1;
                        break;
                    case "L":
                        Head.Position.XPos -= 1;
                        break;
                    default:
                        throw new ArgumentException($"Unknown direction {direction}");
                }
            }

            public void MoveTail()
            {
                if (IsTailAdjacent())
                {
                    return;
                }

                // Direct moves 
        
                // Same Column 
                if (Head.Position.XPos == Tail.Position.XPos)
                {
                    if (Head.Position.YPos == Tail.Position.YPos + 2)
                    {
                        Tail.Position.YPos += 1;
                        return;
                    }
                    else 
                    {
                        Tail.Position.YPos -= 1;
                        return;
                    }
                }

                // Same Row 
                if (Head.Position.YPos == Tail.Position.YPos)
                {
                    if (Head.Position.XPos == Tail.Position.XPos + 2)
                    {
                        Tail.Position.XPos += 1;
                        return;
                    }
                    else
                    {
                        Tail.Position.XPos -= 1;
                        return;
                    }
                }

                // Diagonal moves 

                /*  
                 *   Move 1     Move 2     Move 3     Move 4  
                 *  . . . . .  . H . . .  . . . H .  . . . . .
                 *  H . . . .  . . . . .  . . . . .  . . . . H 
                 *  . . T . .  . . T . .  . . T . .  . . T . .
                 *  . . . . .  . . . . .  . . . . .  . . . . .
                 *  . . . . .  . . . . .  . . . . .  . . . . .
                 *  
                 *   Move 5     Move 6     Move 7     Move 8    
                 *  . . . . .  . . . . .  . . . . .  . . . . .  
                 *  . . . . .  . . . . .  . . . . .  . . . . . 
                 *  . . T . .  . . T . .  . . T . .  . . T . .  
                 *  H . . . .  . . . . .  . . . . .  . . . . H  
                 *  . . . . .  . H . . .  . . . H .  . . . . .  
                 *  
                 */

                if (Head.Position.YPos > Tail.Position.YPos)
                {
                    Tail.Position.YPos += 1;
                }
                else
                {
                    Tail.Position.YPos -= 1;
                }

                if (Head.Position.XPos > Tail.Position.XPos)
                {
                    Tail.Position.XPos += 1;
                }
                else
                {
                    Tail.Position.XPos -= 1;
                }
            }

            public bool IsTailAdjacent()
            {
                return Math.Abs(Head.Position.YPos - Tail.Position.YPos) <= 1 &&
                    Math.Abs(Head.Position.XPos - Tail.Position.XPos) <= 1;
            }

            public int CountUniqueTailPositions()
            {
                return UniqueTailPositions.Count;
            }
        }

        public class Knot 
        { 
            public Position Position { get; }

            public Knot (Position p)
            {
                Position = p;
            }
        }

        public class Position 
        {
            public int XPos { get; set; }

            public int YPos { get; set; }

            public Position(int startingX, int startingY)
            {
                XPos = startingX;
                YPos = startingY;
            }

            public override bool Equals(object obj)
            {
                var p = obj as Position;
                if (p == null)
                {
                    return false;
                }

                return p.XPos == XPos && p.YPos == YPos;
            }
        }
    }
}
