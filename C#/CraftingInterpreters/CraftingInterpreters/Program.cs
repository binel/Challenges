namespace CraftingInterpreters
{
  /*
   * Created by working through "Crafting Interpreters" by Robert Nystrom.
   * 
   * I made some modifications so that programs would never require 
   * pressing the shift key. (Although you do need a num pad to pull that 
   * off) 
   * 
   * 
   * 
   */
  public class Program
  {
    public static void Main(string[] args)
    {
      Prompt();
    }

    private static void Prompt()
    {
      while (true)
      {
        Console.Write(">");
        var line = Console.ReadLine();
        if (line == null) break;
        Run(line);
      }
    }

    private static void Run(string source)
    {
      Scanner s = new Scanner(source);
      var tokens = s.Scan();

      Console.WriteLine(string.Join(',', tokens));
    }
  }
}