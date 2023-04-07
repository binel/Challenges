using TokenType = CraftingInterpreters.Token.TokenType;

namespace CraftingInterpreters
{
  public class Scanner
  {
    private static readonly Dictionary<string, TokenType> _keywords =
      new Dictionary<string, TokenType>()
      {
        {"and", TokenType.AND },
        {"class", TokenType.CLASS },
        {"else", TokenType.ELSE },
        {"false", TokenType.ELSE },
        {"for", TokenType.FOR },
        {"fun", TokenType.FUN },
        {"if", TokenType.IF },
        {"nil", TokenType.NIL },
        {"or", TokenType.OR },
        {"print", TokenType.PRINT },
        {"return", TokenType.RETURN },
        {"super", TokenType.SUPER },
        {"this", TokenType.THIS },
        {"true", TokenType.TRUE },
        {"var", TokenType.VAR },
        {"while", TokenType.WHILE }
      };


    private readonly string _source;
    private readonly List<Token> _tokens = new List<Token>();

    // First char in lexeme being scanned 
    private int _start = 0;
    // position in lexeme 
    private int _position = 0;
    // what source line position is on
    private int _line = 1;

    public Scanner(string source)
    {
      _source = source;
    }

    public List<Token> Scan()
    {
      while (!AtEnd())
      {
        _start = _position;
        ScanToken();
      }

      _tokens.Add(new Token(Token.TokenType.EOF, "", null, _line));
      return _tokens;
    }

    private void ScanToken()
    {
      char c = Advance();
      switch (c)
      {
        case '[': AddToken(TokenType.LEFT_BRACKET); break;
        case ']': AddToken(TokenType.RIGHT_BRACKET); break;
        case ',': AddToken(TokenType.COMMA); break;
        case '.': AddToken(TokenType.DOT); break;
        case '-': AddToken(TokenType.MINUS); break;
        case '+': AddToken(TokenType.PLUS); break;
        case ';': AddToken(TokenType.SEMICOLON); break;
        case '*': AddToken(TokenType.STAR); break;
        case '=':
          AddToken(Match('=') ? TokenType.EQUAL_EQUAL : TokenType.EQUAL);
          break;
        case '/':
          if (Match('/'))
          {
            while (Peek() != '\n' && !AtEnd()) Advance();
          }
          else
          {
            AddToken(TokenType.SLASH);
          }
          break;
        case ' ':
        case '\r':
        case '\t':
          break;
        case '\n':
          _line++;
          break;
        case '\'': String(); break;
        default:
          if (IsDigit(c))
          {
            Number();
          }
          else if  (IsAlpha(c))
          {
            Identifier();
          }
          else 
          {
            Error.Report(_line, $"Unexpected character {c}");
          }
          break;
      }
    }

    private void AddToken(Token.TokenType type)
    {
      AddToken(type, null);
    }

    private void AddToken(Token.TokenType type, object literal)
    {
      var lexeme = _source.Substring(_start, _position - _start);
      var t = new Token(type, lexeme, literal, _line);
      _tokens.Add(t);
    }

    private bool AtEnd()
    {
      return _position >= _source.Length;
    }

    private bool Match(char expected)
    {
      if (AtEnd()) return false;
      if (_source[_position] != expected) return false;

      _position++;
      return true;
    }

    private char Peek()
    {
      if (AtEnd()) return '\0';
      return _source[_position];
    }

    private char PeekNext()
    {
      if (_position + 1 >= _source.Length) return '\0';
      return _source[_position + 1];
    }

    private char Advance()
    {
      return _source[_position++];
    }

    private void String()
    {
      while (Peek() != '\'' && !AtEnd())
      {
        if (Peek() == '\n') _line++;
        Advance();
      }

      if (AtEnd())
      {
        Error.Report(_line, "Unterminated string");
        return;
      }

      Advance();

      var value = _source.Substring(_start + 1, _position - _start - 2);
      AddToken(TokenType.STRING, value);
    }

    private void Number()
    {
      while (IsDigit(Peek())) Advance();

      if (Peek() == '.' && IsDigit(PeekNext())) {
        Advance();
      }

      while (IsDigit(Peek())) Advance();

      AddToken(TokenType.NUMBER, double.Parse(_source.Substring(_start, _position - _start)));
    }

    private void Identifier()
    {
      while (IsAlphaNumeric(Peek())) Advance();

      var text = _source.Substring(_start, _position - _start);
      if (_keywords.ContainsKey(text))
      {
        AddToken(_keywords[text]);
      }
      else
      {
        AddToken(TokenType.IDENTIFIER);
      }
    }

    private bool IsDigit(char c)
    {
      return c >= '0' && c <= '9';
    }

    private bool IsAlpha(char c)
    {
      return (c >= 'a' && c <= 'z') ||
             (c >= 'A' && c <= 'Z') ||
             c == '_';
    }

    private bool IsAlphaNumeric(char c)
    {
      return IsAlpha(c) || IsDigit(c);
    }
  }
}
