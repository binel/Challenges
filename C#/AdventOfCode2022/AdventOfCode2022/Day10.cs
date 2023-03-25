using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
    // TODO there is something slightly wrong with this but it's close enough to
    // read off the output
    public class Day10 : BaseDay
    {
        public override int DayNumber { get; set; } = 10;

        public List<int> InterestingCycles = new List<int> { 20, 60, 100, 140, 180, 220 };

        public override string PuzzlePart1(bool training)
        {
            CPU cpu = new CPU();
            cpu.Program = GetInput(training);
            int sumStrength = 0;
            while(!cpu.ProgramComplete)
            {
                cpu.Tick();
                if (InterestingCycles.Contains(cpu.Cycle))
                {
                    sumStrength += cpu.SignalStrength;
                }
            }

            return sumStrength.ToString();
        }

        public override string PuzzlePart2(bool training)
        {
            CPU cpu = new CPU();
            cpu.Program = GetInput(training);
            string output = "";
            while (!cpu.ProgramComplete)
            {
                cpu.Tick();

                if (cpu.IsPixelDrawn)
                {
                    output += "#";
                }
                else
                {
                    output += ".";
                }

                if (cpu.Cycle % 40 == 0)
                {
                    output += "\n";
                }
            }

            Console.Write(output);
            return output;
        }

        public class CPU
        {
            public int Cycle { get; set; } = 0;

            public int Register { get; set; } = 1;

            public int MidCycleRegisterValue { get; set; } = 1;

            public string CurrentOperation { get; set; } 

            public bool MidOperation { get; set; }

            public string[] Program { get; set; }

            public int InstructionPointer { get; set; } = -1;

            public bool ProgramComplete { get; set; }

            public int CrtHorizontalPosition => Cycle % 40;

            public bool IsPixelDrawn => Math.Abs(MidCycleRegisterValue - CrtHorizontalPosition) < 2;

            public int SignalStrength => MidCycleRegisterValue * Cycle;

            public void Tick()
            {
                Cycle++;
                MidCycleRegisterValue = Register;
                if (!MidOperation)
                {
                    InstructionPointer++;

                    if (InstructionPointer >= Program.Length)
                    {
                        ProgramComplete = true;
                        return;
                    }

                    var input = Program[InstructionPointer];

                    if (input == "noop")
                    {
                        return;
                    }
                    MidOperation = true;
                    return;
                }

                Register += int.Parse(Program[InstructionPointer].Split(" ")[1]);
                MidOperation = false;
            }
        }
    }
}
