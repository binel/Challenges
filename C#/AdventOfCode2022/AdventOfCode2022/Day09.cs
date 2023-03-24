//#define Verbose

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
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
            string[] lines = GetInput(training);
            Simulation s = new Simulation(2);
            foreach (var line in lines)
            {
                s.ExecuteMove(line);
            }

            return s.CountUniqueTailPositions().ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            string[] lines = GetInput(training);
            Simulation s = new Simulation(10);
            foreach (var line in lines)
            {
                s.ExecuteMove(line);
            }

            return s.CountUniqueTailPositions().ToString();
        }

        public class Simulation
        {
            public int SimulationStep { get; set; } = 0;

            public List<int> StepsToPrint { get; set; } = new List<int>();

            public List<Position> UniqueTailPositions { get; } = new List<Position>();

            public List<Knot> Knots = new List<Knot>();

            public Knot Tail { get { return Knots[Knots.Count - 1]; } }

            public int MinXPos { get; set; } = 0;

            public int MaxXPos { get; set; } = 0;

            public int MinYPos { get; set; } = 0;

            public int MaxYPos { get; set; } = 0;

            public Simulation(int numKnots)
            {
                for (int i = 0; i < numKnots; i++)
                {
                    Knots.Add(new Knot(new Position(0, 0), i.ToString()));
                }
            }

            public void ExecuteMove(string move)
            {
                var split = move.Split(" ");
                var direction = split[0];
                int distance = int.Parse(split[1]);

                for (int i = 0; i < distance; i++)
                {
                    for (int k = 0; k < Knots.Count; k++)
                    {
                        try {
                            SimulationStep += 1;
                            MoveKnot(k, direction);
                            UpdateSimulationBoundaries();
                            if (StepsToPrint.Contains(SimulationStep))
                            {
                                Console.WriteLine($"Simulation step {SimulationStep}");
                                Console.WriteLine($"Moving knot {k}");
                                Console.WriteLine($"Move: {move} ({i + 1}/{distance})");
                                UpdateSimulationBoundaries();
                                PrintSimulationState();
                            }
                        }
                        catch
                        {
                            Console.WriteLine($"Simulation step {SimulationStep}");
                            Console.WriteLine($"Move: {move} ({i + 1}/{distance})");
                            Console.WriteLine($"Could knot move knot {k}");
                            Console.WriteLine($"knot {k} position: ({Knots[k].Position.XPos},{Knots[k].Position.YPos})");
                            if (k != 0)
                            {
                                Console.WriteLine($"knot {k - 1} position: ({Knots[k - 1].Position.XPos},{Knots[k - 1].Position.YPos})");
                            }
                            UpdateSimulationBoundaries();
                            PrintSimulationState();
                            throw;
                        }
                            
                    }
                    if (UniqueTailPositions.Find(p => p.Equals(Tail.Position)) == null)
                    {
                        UniqueTailPositions.Add(new Position(Tail.Position.XPos, Tail.Position.YPos));
                    }

                }
                #if Verbose
                Console.WriteLine(move);
                UpdateSimulationBoundaries();
                PrintSimulationState();
                #endif
            }

            public void UpdateSimulationBoundaries()
            {
                foreach (var k in Knots)
                {
                    MinXPos = Math.Min(MinXPos, k.Position.XPos);
                    MinYPos = Math.Min(MinYPos, k.Position.YPos);
                    MaxXPos = Math.Max(MaxXPos, k.Position.XPos);
                    MaxYPos = Math.Max(MaxYPos, k.Position.YPos);
                }
            }

            public void PrintSimulationState()
            {
                for (int y = MaxYPos; y >= MinYPos; y--)
                {
                    string line = "";
                    for (int x = MinXPos; x <= MaxXPos; x++)
                    {
                        var k = Knots.Find(p => p.Position.XPos == x && p.Position.YPos == y);
                        Position pos = new Position(x, y);
                        if (k != null)
                        {
                            line += k.Name;
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
                Console.WriteLine();
            }

            public Move ParseMove(string move)
            {
                switch (move)
                {
                    case "U":
                        return Move.N;
                    case "D":
                        return Move.S;
                    case "L":
                        return Move.W;
                    case "R":
                        return Move.E;
                    default:
                        throw new ArgumentException($"unknown move {move}");
                }
            }

            public void MoveKnot(int knotNumber, string direction)
            {
                if (knotNumber == 0)
                {
                    var move = ParseMove(direction);
                    #if Verbose
                        Console.WriteLine($"Moving knot {knotNumber} with move {move}");
                    #endif
                    Knots[0].Position.YPos += YPositionMap[move];
                    Knots[0].Position.XPos += XPositionMap[move];
                }
                else 
                {
                    var relationship = DetermineRelationship(Knots[knotNumber], Knots[knotNumber - 1]);
                    var move = Moves[relationship];
                    #if Verbose
                        Console.WriteLine($"Moving knot {knotNumber} with move {move}");
                    #endif
                    Knots[knotNumber].Position.YPos += YPositionMap[move];
                    Knots[knotNumber].Position.XPos += XPositionMap[move];
                }
            }

            public int CountUniqueTailPositions()
            {
                return UniqueTailPositions.Count;
            }

            /// <summary>
            /// Given the current relationship between two knots, determine the move of the 
            /// second knot
            /// </summary>
            Dictionary<Relation, Move> Moves = new Dictionary<Relation, Move>()
            {
                // Adjacent or coincident, no move required
                { Relation.COINCIDENT, Move.STATIONARY}, { Relation.ADJACENT_N, Move.STATIONARY},
                { Relation.ADJACENT_NE, Move.STATIONARY}, { Relation.ADJACENT_E, Move.STATIONARY},
                { Relation.ADJACENT_SE, Move.STATIONARY}, { Relation.ADJACENT_S, Move.STATIONARY},
                { Relation.ADJACENT_SW, Move.STATIONARY}, { Relation.ADJACENT_W, Move.STATIONARY},
                { Relation.ADJACENT_NW, Move.STATIONARY},
            
                // Direct column / row 
                { Relation.SEPARATED_N, Move.N}, { Relation.SEPARATED_E, Move.E},
                { Relation.SEPARATED_S, Move.S}, { Relation.SEPARATED_W, Move.W},

                // Diagnoal moves 
                // NW
                { Relation.SEPARATED_WNW, Move.NW}, { Relation.SEPARATED_NW, Move.NW},
                { Relation.SEPARATED_NNW, Move.NW},
                // NE
                { Relation.SEPARATED_NNE, Move.NE}, { Relation.SEPARATED_NE, Move.NE},
                { Relation.SEPARATED_ENE, Move.NE},
                // SE
                { Relation.SEPARATED_ESE, Move.SE}, { Relation.SEPARATED_SE, Move.SE},
                { Relation.SEPARATED_SSE, Move.SE},
                // SW
                { Relation.SEPARATED_WSW, Move.SW}, { Relation.SEPARATED_SW, Move.SW},
                { Relation.SEPARATED_SSW, Move.SW},
            };

            /// <summary>
            /// Determine the relationship of k1 with respect to k2
            /// </summary>
            public Relation DetermineRelationship(Knot k1, Knot k2)
            {
                var p1 = k1.Position;
                var p2 = k2.Position;
                if (p1.XPos == p2.XPos && p1.YPos == p2.YPos) return Relation.COINCIDENT;

                /*
                 * . . . . . 
                 * . . . . . 
                 * X X 1 X X 
                 * . . . . . 
                 * . . . . .
                 */

                if (p1.XPos == p2.XPos)
                {
                    if (p1.YPos + 1 == p2.YPos) return Relation.ADJACENT_N;
                    if (p1.YPos + 2 == p2.YPos) return Relation.SEPARATED_N;
                    if (p1.YPos - 1 == p2.YPos) return Relation.ADJACENT_S;
                    if (p1.YPos - 2 == p2.YPos) return Relation.SEPARATED_S;
                }

                /*
                 * . . X . . 
                 * . . X . . 
                 * O O 1 O O 
                 * . . X . . 
                 * . . X . .
                 */
                if (p1.YPos == p2.YPos)
                {
                    if (p1.XPos + 1 == p2.XPos) return Relation.ADJACENT_E;
                    if (p1.XPos + 2 == p2.XPos) return Relation.SEPARATED_E;
                    if (p1.XPos - 1 == p2.XPos) return Relation.ADJACENT_W;
                    if (p1.XPos - 2 == p2.XPos) return Relation.SEPARATED_W;
                }

                /*
                 * . . O . . 
                 * X X O X X 
                 * O O 1 O O 
                 * . . O . . 
                 * . . O . .
                 */
                if (p1.YPos + 1 == p2.YPos)
                {
                    if (p1.XPos + 1 == p2.XPos) return Relation.ADJACENT_NE;
                    if (p1.XPos + 2 == p2.XPos) return Relation.SEPARATED_ENE;
                    if (p1.XPos - 1 == p2.XPos) return Relation.ADJACENT_NW;
                    if (p1.XPos - 2 == p2.XPos) return Relation.SEPARATED_WNW;
                }

                /*
                 * X X O X X 
                 * O O O O O 
                 * O O 1 O O 
                 * . . O . . 
                 * . . O . .
                 */
                if (p1.YPos + 2 == p2.YPos)
                {
                    if (p1.XPos + 1 == p2.XPos) return Relation.SEPARATED_NNE;
                    if (p1.XPos + 2 == p2.XPos) return Relation.SEPARATED_NE;
                    if (p1.XPos - 1 == p2.XPos) return Relation.SEPARATED_NNW;
                    if (p1.XPos - 2 == p2.XPos) return Relation.SEPARATED_NW;
                }

                /*
                 * O O O O O 
                 * O O O O O 
                 * O O 1 O O 
                 * X X O X X 
                 * . . O . .
                 */
                if (p1.YPos - 1 == p2.YPos)
                {
                    if (p1.XPos + 1 == p2.XPos) return Relation.ADJACENT_SE;
                    if (p1.XPos + 2 == p2.XPos) return Relation.SEPARATED_ESE;
                    if (p1.XPos - 1 == p2.XPos) return Relation.ADJACENT_SW;
                    if (p1.XPos - 2 == p2.XPos) return Relation.SEPARATED_WSW;
                }

                /*
                 * O O O O O 
                 * O O O O O 
                 * O O 1 O O 
                 * O O O O O 
                 * X X O X X
                 */
                if (p1.YPos - 2 == p2.YPos)
                {
                    if (p1.XPos + 1 == p2.XPos) return Relation.SEPARATED_SSE;
                    if (p1.XPos + 2 == p2.XPos) return Relation.SEPARATED_SE;
                    if (p1.XPos - 1 == p2.XPos) return Relation.SEPARATED_SSW;
                    if (p1.XPos - 2 == p2.XPos) return Relation.SEPARATED_SW;
                }

                throw new ArgumentException("Invalid state");
            }

            /// <summary>
            /// Given a move, this dictionary returns what should happen to that knot's 
            /// X position to execute the move
            /// </summary>
            Dictionary<Move, int> XPositionMap = new Dictionary<Move, int>()
            {
                { Move.STATIONARY, 0}, {Move.N, 0}, {Move.S, 0},
                { Move.E, 1 }, { Move.NE, 1}, {Move.SE, 1},
                { Move.W, -1}, { Move.NW, -1}, {Move.SW, -1}
            };

            /// <summary>
            /// Given a move, this dictionary returns what should happen to that knot's 
            /// Y position to execute the move
            /// </summary>
            Dictionary<Move, int> YPositionMap = new Dictionary<Move, int>()
            {
                { Move.STATIONARY, 0}, {Move.E, 0}, {Move.W, 0},
                { Move.N, 1 }, { Move.NE, 1}, {Move.NW, 1},
                { Move.S, -1}, { Move.SE, -1}, {Move.SW, -1}
            };
        }

        public class Knot 
        { 
            public Position Position { get; }

            public string Name { get; }

            public Knot (Position p, string name)
            {
                Position = p;
                Name = name;
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

        public enum Move
        {
            STATIONARY,
            N,
            NE,
            E,
            SE,
            S,
            SW,
            W,
            NW
        }

        /// <summary>
        /// Lists all possible ways two knots can be related to each other 
        /// position-wise 
        /// </summary>
        public enum Relation
        {
            COINCIDENT, // On top of each other 

            // 1 cell away 
            ADJACENT_N,
            ADJACENT_NE,
            ADJACENT_E,
            ADJACENT_SE,
            ADJACENT_S,
            ADJACENT_SW,
            ADJACENT_W,
            ADJACENT_NW,

            // 2 cells away
            SEPARATED_N,
            SEPARATED_NNE,
            SEPARATED_NE,
            SEPARATED_ENE,
            SEPARATED_E,
            SEPARATED_ESE,
            SEPARATED_SE,
            SEPARATED_SSE,
            SEPARATED_S,
            SEPARATED_SSW,
            SEPARATED_SW,
            SEPARATED_WSW,
            SEPARATED_W,
            SEPARATED_WNW,
            SEPARATED_NW,
            SEPARATED_NNW
        }
    }
}
