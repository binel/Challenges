using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode2022
{
  public class Day13 : BaseDay
  {
    public override int DayNumber { get; set; } = 13;

    public override string PuzzlePart1(bool training)
    {
      var lines = GetInput(training);

      for (int i = 0; i < lines.Length; i += 2)
      {
        var leftLine = lines[i];
        var rightLine = lines[i + 1];

        
      }

      return "-1";
    }

    public override string PuzzlePart2(bool training)
    {
      return "Not Done";
    }

    class Packet {
      
    }
  }
}
