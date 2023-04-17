using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters
{
  public class Token
  {
    public enum TokenType
    {
      LEFT_BRACKET, RIGHT_BRACKET, COMMA, DOT, MINUS,
      PLUS, STAR, SEMICOLON, SLASH,

      EQUAL, EQUAL_EQUAL,

      IDENTIFIER, STRING, NUMBER,

      AND, CLASS, ELSE, FALSE, FUN, FOR, IF, NIL, OR,
      PRINT, RETURN, SUPER, THIS, TRUE, VAR, WHILE,
      GREATER, GREATER_EQUAL, LESS, LESS_EQUAL, NOT,

      EOF
    }

    public TokenType Type { get; init; }
    public string Lexeme { get; init; }
    public object Literal { get; init; }
    public int Line { get; init; }

    public Token(TokenType type, string lexeme, object literal, int line)
    {
      Type = type;
      Lexeme = lexeme;
      Literal = literal;
      Line = line;
    }

    public override string ToString()
    {
      return $"{Type} {Lexeme} {Literal}";
    }

  }
}
