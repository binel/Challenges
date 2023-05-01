using System;
using System.Text;

namespace CodeGen
{
    internal class Program
    {
        private static readonly string NAMESPACE = "CraftingInterpreters";
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            DefineAst(sb, "Expression", new List<string>
            {
                "Binary   : Expression Left, Token Op, Expression Right",
                "Grouping : Expression Expression",
                "Literal  : object Value",
                "Unary    : Token Op, Expression Right"
            });

            Console.Write(sb.ToString());
        }

        private static void DefineAst(StringBuilder sb, string baseTypeName, List<string> types)
        {
            AppendLine(sb, "// AUTO GENERATED CODE", 0);
            AppendLine(sb, $"namespace {NAMESPACE}", 0);
            AppendLine(sb, "{", 0);
            AppendLine(sb, $"public abstract class {baseTypeName}", 1);
            AppendLine(sb, "{", 1);

            DefineVisitor(sb, baseTypeName, types, 2);

            AppendLine(sb, "public abstract T Accept<T>(Visitor<T> visitor);", 1);

            foreach (var type in types)
            {
                var className = type.Split(":")[0].Trim();
                var fields = type.Split(":")[1].Trim();
                DefineType(sb, baseTypeName, className, fields, 2);
                sb.AppendLine();
            }

            AppendLine(sb, "}", 1);
            AppendLine(sb, "}", 0);
        }

        private static void DefineType(StringBuilder sb, string baseTypeName, string className, string fieldList, int indent)
        {
            AppendLine(sb, $"public class {className} : {baseTypeName}", indent);
            AppendLine(sb, "{", indent);

            var fields = fieldList.Split(",");
            foreach (var field in fields)
            {
                AppendLine(sb, $"public {field.Trim()} {{get; init;}}", indent + 1);
                sb.AppendLine();
            }

            AppendLine(sb, "public override T Accept<T>(Visitor<T> visitor)", indent + 1);
            AppendLine(sb, "{", indent + 1);
            AppendLine(sb, $"return visitor.Visit{className}{baseTypeName}(this);", indent + 2);
            AppendLine(sb, "}", indent + 1);

            AppendLine(sb, "}", indent);
        }

        private static void DefineVisitor(StringBuilder sb, string baseTypeName, List<string> types, int indent)
        {
            AppendLine(sb, "public interface Visitor<T>", indent);
            AppendLine(sb, "{", indent);

            foreach (var type in types)
            {
                var typeName = type.Split(":")[0].Trim();
                AppendLine(sb, $"T Visit{typeName}{baseTypeName} ({typeName} {baseTypeName.ToLower()});", indent + 1);
            }



            AppendLine(sb, "}", indent);
        }

        private static void AppendLine(StringBuilder sb, string str, int tabLevel)
        {
            sb.AppendLine($"{new string('\t', tabLevel)}{str}");
        }
    }
}