using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Collections;
using System.Runtime.InteropServices;

namespace AdventOfCode2022
{
    public class Day05 : BaseDay
    {
        public override int DayNumber { get; set; } = 5;

        public override string PuzzlePart1(bool training)
        {
            return MainLogic(training, true);
        }

        public override string PuzzlePart2(bool training)
        {
            return MainLogic(training, false);
        }

        private string MainLogic(bool training, bool moveOneByOne)
        {
            string[] lines = GetInput(training);
            lines = lines.Reverse().ToArray(); // It's easier to work backwards 
            Stack<Move> moves = new Stack<Move>();
            Supplies supplies = new Supplies();
            bool stacksInitialized = false;

            foreach (var line in lines)
            {
                if (line.StartsWith("move"))
                {
                    moves.Push(new Move(line));
                }
                else if (string.IsNullOrWhiteSpace(line) || line.StartsWith(" 1"))
                {
                    continue;
                }
                else if (!stacksInitialized)
                {
                    // We know the first line (from bottom up) is full of crates
                    var split = line.Split(" ");
                    for (int i = 1; i <= split.Length; i++)
                    {
                        supplies.AddStack(i, new Stack());
                        supplies.GetStack(i).Add(new Crate(split[i - 1]));
                    }
                    stacksInitialized = true;
                }
                else
                {
                    // Now we might have gaps, so we have to check directly
                    int stack = 1;
                    for (int i = 1; i < line.Length; i += 4, stack += 1)
                    {
                        if (line[i] != ' ')
                        {
                            supplies
                                .GetStack(stack)
                                .Add(new Crate { Name = line[i].ToString() });
                        }
                    }
                }
            }

            while (moves.TryPop(out Move m))
            {
                supplies.ExecuteMove(m, moveOneByOne);
            }
            return supplies.GetTopCrates();
        }

        public class Crate
        {
            public string Name { get; set; }

            public Crate(string crateSubstring)
            {
                Name = crateSubstring.Substring(1, 1);
            }

            public Crate() { }
        }

        public class Stack
        {
            public Stack<Crate> Crates { get; set; } = new Stack<Crate>();

            public void Add(Crate crate)
            {
                Crates.Push(crate);
            }

            public Crate Remove()
            {
                return Crates.Pop();
            }

            public Crate Peek()
            {
                return Crates.Peek();
            }
        }

        public class Supplies
        {
            public Dictionary<int, Stack> Stacks = new Dictionary<int, Stack>();
            public int NumStacks = 0;
            
            public void AddStack(int index, Stack stack)
            {
                Stacks[index] = stack;
                NumStacks++;
            }

            public Stack GetStack(int index)
            {
                return Stacks[index];
            }

            public void ExecuteMove(Move m, bool moveOneByOne)
            {
                if (moveOneByOne)
                {
                    for (int i = 0; i < m.StackCount; i++)
                    {
                        Crate c = Stacks[m.FromIndex].Remove();
                        Stacks[m.ToIndex].Add(c);
                    }
                }
                else {
                    Stack<Crate> intermediateStack = new Stack<Crate>();
                    for (int i = 0; i < m.StackCount; i++)
                    {
                        intermediateStack.Push(Stacks[m.FromIndex].Remove());
                    }
                    for (int i = 0; i < m.StackCount; i++)
                    {
                        Stacks[m.ToIndex].Add(intermediateStack.Pop());
                    }
                }

            }

            public string GetTopCrates()
            {
                string ret = "";
                for (int i = 1; i <= NumStacks; i++)
                {
                    Crate c = Stacks[i].Peek();
                    ret += c.Name;
                }
                return ret;
            }
        }

        public class Move
        {
            public Move(string line)
            {
                var split = line.Split(" ");
                StackCount = int.Parse(split[1]);
                FromIndex = int.Parse(split[3]);
                ToIndex = int.Parse(split[5]);
            }

            public int StackCount { get; set; }
            
            public int FromIndex { get; set; }

            public int ToIndex { get; set; }
        }
    }
}
