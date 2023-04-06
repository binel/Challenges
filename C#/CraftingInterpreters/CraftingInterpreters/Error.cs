using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters
{
  public static class Error
  {
    public static bool ErrorReported = false;

    public static void Report(int line, string message)
    {
      Report(line, "", message);
    }

    public static void Report(int line, string where, string message)
    {
      Console.WriteLine($"[line {line}] Error {where}: {message}");
      ErrorReported = true;
    }
  }
}
