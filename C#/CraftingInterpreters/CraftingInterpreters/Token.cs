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
      GREATER, GREATER_EQUAL, LESS, LESS_EQUAL,

      EOF
    }

    private readonly TokenType _type;
    private readonly string _lexeme;
    private readonly object _literal;
    private readonly int _line;

    public Token(TokenType type, string lexeme, object literal, int line)
    {
      _type = type;
      _lexeme = lexeme;
      _literal = literal;
      _line = line;
    }

    public override string ToString()
    {
      return $"{_type} {_lexeme} {_literal}";
    }

  }
}
