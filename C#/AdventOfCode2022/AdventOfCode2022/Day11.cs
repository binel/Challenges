using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    public class Day11 : BaseDay
    {
        public override int DayNumber { get; set; } = 11;

        public override string PuzzlePart1(bool training)
        {
            var lines = GetInput(training);

            List<Monkey> monkies = new List<Monkey>();
            foreach (var line in lines)
            {
                if (line.StartsWith("Monkey"))
                {
                    monkies.Add(new Monkey());
                }

                var split = line.Split(" ");

                if (line.Contains("Starting items"))
                {
                    for (int i = 4; i < split.Length; i++)
                    {
                        var item = split[i].Replace(",", "");
                        monkies.Last().Items.Add(int.Parse(item));
                    }
                }

                if (line.Contains("Operation"))
                {
                    var op = split[6];
                    var rhs = split[7];

                    Func<BigInteger, BigInteger> opfunc;

                    if (op == "*")
                    {
                        if (rhs == "old")
                        {
                            opfunc = (old) => old * old;
                        }
                        else
                        {
                            int i = int.Parse(rhs);
                            opfunc = (old) => old * i;
                        }
                    }
                    else
                    {
                        if (rhs == "old")
                        {
                            opfunc = (old) => old + old;
                        }
                        else 
                        {
                            int i = int.Parse(rhs);
                            opfunc = (old) => old + i;
                        }
                    }

                    monkies.Last().Operation = opfunc;
                }

                if (line.Contains("Test"))
                {
                    int i = int.Parse(split[5]);
                    monkies.Last().Test = (old) => old % i == 0; 
                }

                if (line.Contains("If true:"))
                {
                    monkies.Last().TrueMonkey = int.Parse(split[9]);
                }

                if (line.Contains("If false:"))
                {
                    monkies.Last().FalseMonkey = int.Parse(split[9]);
                }
            }

            Console.WriteLine($"Start:");
            for (int i = 0; i < monkies.Count; i++)
            {
                Console.WriteLine($"Monkey {i}: " + string.Join(",", monkies[i].Items));
            }

            for (int round = 0; round < 20; round++)
            {
                for (int monkeyIndex = 0; monkeyIndex < monkies.Count; monkeyIndex++)
                {
                    Console.WriteLine($"Monkey {monkeyIndex}");
                    Monkey monkey = monkies[monkeyIndex];
                    for (int itemIndex = 0; itemIndex < monkey.Items.Count; itemIndex++)
                    {
                        BigInteger item = monkey.Items[itemIndex];
                        Console.WriteLine($"\tMonkey inspects an item with a worry level of {item}");
                        item = monkey.Operation(item);
                        Console.WriteLine($"\tWorry level becomes {item}");
                        monkey.InspectionCount++;
                        item /= 3;
                        Console.WriteLine($"\tWorry level is divided by 3 to {item}");
                        int throwMonkey = monkey.Test(item) ? monkey.TrueMonkey : monkey.FalseMonkey;
                        monkies[throwMonkey].Items.Add(item);
                        Console.WriteLine($"\tItem in thrown to monkey {throwMonkey}");
                    }
                    monkey.Items.RemoveAll(p => true);
                }

                Console.WriteLine($"After round {round}:");
                for (int i = 0; i < monkies.Count; i++)
                {
                    Console.WriteLine($"Monkey {i}: " + string.Join(",", monkies[i].Items));
                }
            }

            for (int i = 0; i < monkies.Count; i++)
            {
                Console.WriteLine($"Monkey {i} inspected items {monkies[i].InspectionCount} times");
            }

            var sort = monkies.OrderByDescending(m => m.InspectionCount).Take(2).ToList();

            return (sort[0].InspectionCount * sort[1].InspectionCount).ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            var lines = GetInput(training);

            List<Monkey> monkies = new List<Monkey>();
            foreach (var line in lines)
            {
                if (line.StartsWith("Monkey"))
                {
                    monkies.Add(new Monkey());
                }

                var split = line.Split(" ");

                if (line.Contains("Starting items"))
                {
                    for (int i = 4; i < split.Length; i++)
                    {
                        var item = split[i].Replace(",", "");
                        monkies.Last().Items.Add(int.Parse(item));
                    }
                }

                if (line.Contains("Operation"))
                {
                    var op = split[6];
                    var rhs = split[7];

                    Func<BigInteger, BigInteger> opfunc;

                    if (op == "*")
                    {
                        if (rhs == "old")
                        {
                            opfunc = (old) => old * old;
                        }
                        else
                        {
                            int i = int.Parse(rhs);
                            opfunc = (old) => old * i;
                        }
                    }
                    else
                    {
                        if (rhs == "old")
                        {
                            opfunc = (old) => old + old;
                        }
                        else
                        {
                            int i = int.Parse(rhs);
                            opfunc = (old) => old + i;
                        }
                    }

                    monkies.Last().Operation = opfunc;
                }

                if (line.Contains("Test"))
                {
                    int i = int.Parse(split[5]);
                    monkies.Last().Test = (old) => old % i == 0;
                    monkies.Last().DivisibleNumber = i;
                }

                if (line.Contains("If true:"))
                {
                    monkies.Last().TrueMonkey = int.Parse(split[9]);
                }

                if (line.Contains("If false:"))
                {
                    monkies.Last().FalseMonkey = int.Parse(split[9]);
                }
            }

            for (int round = 1; round < 1001; round++)
            {
                for (int monkeyIndex = 0; monkeyIndex < monkies.Count; monkeyIndex++)
                {
                    Monkey monkey = monkies[monkeyIndex];
                    for (int itemIndex = 0; itemIndex < monkey.Items.Count; itemIndex++)
                    {
                        BigInteger item = monkey.Items[itemIndex];
                        item = monkey.Operation(item);
                        monkey.InspectionCount++;
                        //item %= monkey.DivisibleNumber;
                        int throwMonkey = monkey.Test(item) ? monkey.TrueMonkey : monkey.FalseMonkey;
                        monkies[throwMonkey].Items.Add(item);
                    }
                    monkey.Items.RemoveAll(p => true);
                }
                if (round == 1 || round == 20 || round == 1000 || round == 2000)
                {
                    Console.WriteLine($"After round {round}:");
                    for (int i = 0; i < monkies.Count; i++)
                    {
                        Console.WriteLine($"Monkey {i} inspected items {monkies[i].InspectionCount} times");
                    }
                }

            }

            for (int i = 0; i < monkies.Count; i++)
            {
                Console.WriteLine($"Monkey {i} inspected items {monkies[i].InspectionCount} times");
            }

            var sort = monkies.OrderByDescending(m => m.InspectionCount).Take(2).ToList();

            return (sort[0].InspectionCount * sort[1].InspectionCount).ToString();
        }

        class Monkey
        {
            public List<BigInteger> Items = new List<BigInteger>();

            public int DivisibleNumber { get; set; }

            public Func<BigInteger, BigInteger> Operation { get; set; }

            public Func<BigInteger, bool> Test { get; set; }

            public int TrueMonkey { get; set; }

            public int FalseMonkey { get; set; }

            public int InspectionCount { get; set; }
        }
    }
}
