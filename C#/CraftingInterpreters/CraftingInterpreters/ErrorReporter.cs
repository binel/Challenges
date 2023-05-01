using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters
{
    public static class ErrorReporter
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

        public static void Error(Token token, string message)
        {
            if (token.Type == Token.TokenType.EOF)
            {
                Report(token.Line, " at end", message);
            }
            else
            {
                Report(token.Line, $" at '{token.Lexeme}' {message}");
            }
        }
    }
}
