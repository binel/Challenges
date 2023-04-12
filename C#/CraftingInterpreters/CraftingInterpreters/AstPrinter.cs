using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace CraftingInterpreters
{
    public class AstPrinter : Expression.Visitor<string>
    {
        public string Print(Expression expression)
        {
            return expression.Accept(this);
        }

        public string VisitBinaryExpression(Expression.Binary expression)
        {
            return Parenthesize(expression.Op.Lexeme, expression.Left, expression.Right);
        }

        public string VisitGroupingExpression(Expression.Grouping expression)
        {
            return Parenthesize("group", expression.Expression);
        }

        public string VisitLiteralExpression(Expression.Literal expression)
        {
            if (expression.Value == null) return "nil";
            return expression.Value.ToString();
        }

        public string VisitUnaryExpression(Expression.Unary expression)
        {
            return Parenthesize(expression.Op.Lexeme, expression.Right);
        }

        private string Parenthesize(string name, params Expression[] expressions)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("(");
            sb.Append(name);
            foreach (var expression in expressions)
            {
                sb.Append(" ");
                sb.Append(expression.Accept(this));
            }
            sb.Append(")");

            return sb.ToString();
        }
    }
}
