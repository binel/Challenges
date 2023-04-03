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
      LEFT_PAREN, RIGHT_PAREN, LEFT_BRACE,
      RIGHT_BRACE, COMMA, DOT, MINUS,
      PLUS, SEMICOLON, SLASH, STAR,

      BANG, BANG_EQUAL, EQUAL, EQUAL_EQUAL,
      GREATER, GREATER_EQUAL, LESS, LESS_EQUAL,

      IDENTIFIER, STRING, NUMBER,

      AND, CLASS, ELSE, FALSE, FUN, FOR, IF,
      NIL, OR, PRINT, RETURN, SUPER, THIS, TRUE,
      VAR, WHILE,

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
