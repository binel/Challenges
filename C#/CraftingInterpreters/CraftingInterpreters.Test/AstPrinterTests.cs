using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters.Test
{
    public class AstPrinterTests
    {
        [Test]
        public void HappyPath()
        {
            Expression e = new Expression.Binary
            {
                Op = new Token(Token.TokenType.STAR, "*", null, 1),
                Left = new Expression.Unary
                {
                    Op = new Token(Token.TokenType.MINUS, "-", null, 1),
                    Right = new Expression.Literal
                    {
                        Value = "123"
                    }
                },
                Right = new Expression.Grouping
                {
                    Expression = new Expression.Literal
                    {
                        Value = "41.12"
                    }
                }
            };

            var print = new AstPrinter().Print(e);

            Assert.That(print, Is.EqualTo("(* (- 123) (group 41.12))"));
        }
    }
}
