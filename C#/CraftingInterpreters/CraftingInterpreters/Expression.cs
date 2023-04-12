// AUTO GENERATED CODE
namespace CraftingInterpreters
{
    public abstract class Expression
    {
        public interface Visitor<T>
        {
            T VisitBinaryExpression(Binary expression);
            T VisitGroupingExpression(Grouping expression);
            T VisitLiteralExpression(Literal expression);
            T VisitUnaryExpression(Unary expression);
        }
        public abstract T Accept<T>(Visitor<T> visitor);
        public class Binary : Expression
        {
            public Expression Left { get; init; }

            public Token Op { get; init; }

            public Expression Right { get; init; }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitBinaryExpression(this);
            }
        }

        public class Grouping : Expression
        {
            public Expression Expression { get; init; }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitGroupingExpression(this);
            }
        }

        public class Literal : Expression
        {
            public object Value { get; init; }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitLiteralExpression(this);
            }
        }

        public class Unary : Expression
        {
            public Token Op { get; init; }

            public Expression Right { get; init; }

            public override T Accept<T>(Visitor<T> visitor)
            {
                return visitor.VisitUnaryExpression(this);
            }
        }

    }
}