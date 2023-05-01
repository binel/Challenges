using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters
{
    public class Parser
    {
        private class ParseError : Exception { }

        private readonly List<Token> _tokens;
        private int _current = 0;

        public Parser(List<Token> tokens)
        {
            _tokens = tokens;
        }

        public Expression Parse()
        {
            try
            {
                return Expression();
            }
            catch (ParseError e)
            {
                return null;
            }
        }

        private Expression Expression()
        {
            return Equality();
        }

        private Expression Equality()
        {
            Expression expression = Comparison();

            // Book had not equal here, need a way to do that
            while (Match(Token.TokenType.EQUAL))
            {
                expression = new Expression.Binary
                {
                    Left = expression,
                    Op = Previous(),
                    Right = Comparison()
                };
            }

            return expression;
        }

        private Expression Comparison()
        {
            Expression expression = Term();

            while (Match(
                Token.TokenType.GREATER, Token.TokenType.GREATER_EQUAL,
                Token.TokenType.LESS, Token.TokenType.LESS_EQUAL))
            {
                expression = new Expression.Binary
                {
                    Left = expression,
                    Op = Previous(),
                    Right = Term()
                };
            }

            return expression;
        }

        private Expression Term()
        {
            Expression expression = Factor();

            while (Match(Token.TokenType.PLUS, Token.TokenType.MINUS))
            {
                expression = new Expression.Binary
                {
                    Left = expression,
                    Op = Previous(),
                    Right = Factor()
                };
            }

            return expression;
        }

        private Expression Factor()
        {
            Expression expression = Unary();

            while (Match(Token.TokenType.SLASH, Token.TokenType.STAR))
            {
                expression = new Expression.Binary
                {
                    Left = expression,
                    Op = Previous(),
                    Right = Unary()
                };
            }

            return expression;
        }

        private Expression Unary()
        { 
            if (Match(Token.TokenType.MINUS, Token.TokenType.NOT))
            {
                return new Expression.Unary
                {
                    Op = Previous(),
                    Right = Unary()
                };
            }

            return Primary();
        }

        private Expression Primary()
        {
            if (Match(Token.TokenType.FALSE))
            {
                return new Expression.Literal
                {
                    Value = false
                };
            }

            if (Match(Token.TokenType.TRUE))
            {
                return new Expression.Literal
                {
                    Value = true
                };
            }

            if (Match(Token.TokenType.NIL))
            {
                return new Expression.Literal
                {
                    Value = null
                };
            }

            if (Match(Token.TokenType.NUMBER, Token.TokenType.STRING))
            {
                return new Expression.Literal
                {
                    Value = Previous().Literal
                };
            }

            if (Match(Token.TokenType.LEFT_BRACKET))
            {
                Expression expression = Expression();

                Consume(Token.TokenType.RIGHT_BRACKET, "Expected '}' after expression.");

                return new Expression.Grouping
                {
                    Expression = expression
                };
            }

            throw Error(Peek(), "Expected expression.");
        }

        private void Synchronize()
        {
            Advance();

            while (!IsAtEnd())
            {
                if (Previous().Type == Token.TokenType.SEMICOLON) return;

                switch (Peek().Type)
                {
                    case Token.TokenType.CLASS:
                    case Token.TokenType.FUN:
                    case Token.TokenType.VAR:
                    case Token.TokenType.FOR:
                    case Token.TokenType.IF:
                    case Token.TokenType.WHILE:
                    case Token.TokenType.PRINT:
                    case Token.TokenType.RETURN:
                        return;
                }

                Advance();
            }
        }

        private Token Consume(Token.TokenType type, string message)
        {
            if (Check(type)) return Advance();
            throw Error(Peek(), message);
        }


        private ParseError Error(Token token, string message)
        {
            ErrorReporter.Error(token, message);
            return new ParseError();
        }

        private bool Match(params Token.TokenType[] types)
        {
            foreach (var type in types)
            {
                if (Check(type))
                {
                    Advance();
                    return true;
                }
            }
            return false;
        }

        private bool Check(Token.TokenType type)
        {
            if (IsAtEnd()) return false;
            return Peek().Type == type;
        }

        private Token Advance()
        {
            if (!IsAtEnd()) _current++;
            return Previous();
        }

        private bool IsAtEnd()
        {
            return Peek().Type == Token.TokenType.EOF;
        }

        private Token Peek()
        {
            return _tokens[_current];
        }

        private Token Previous()
        {
            return _tokens[_current - 1];
        }
    }


}
