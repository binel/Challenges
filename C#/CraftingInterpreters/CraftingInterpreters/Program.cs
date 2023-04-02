namespace CraftingInterpreters
{
    public class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Enter program.");
                return;
            }

            Run(args[0]);   
        }

        private static void Run(string source)
        {

        }
    }
}