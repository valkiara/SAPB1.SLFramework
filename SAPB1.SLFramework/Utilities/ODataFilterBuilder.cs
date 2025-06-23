using System.Linq.Expressions;
using System.Reflection;

namespace SAPB1.SLFramework.Utilities
{
    public static class ODataFilterBuilder
    {
        public static string ToODataFilter<T>(Expression<Func<T, bool>> expression)
        {
            return Visit(expression.Body);
        }

        private static string Visit(Expression expr)
        {
            return expr switch
            {
                BinaryExpression binary => VisitBinary(binary),
                MethodCallExpression method => VisitMethodCall(method),
                MemberExpression member => VisitMember(member),
                ConstantExpression constant => FormatConstant(constant.Value),
                UnaryExpression unary => VisitUnary(unary),
                _ => throw new NotSupportedException($"Expression '{expr}' is not supported.")
            };
        }

        private static string VisitBinary(BinaryExpression expr)
        {
            var left = Visit(expr.Left);
            var right = Visit(expr.Right);
            var op = GetOperator(expr.NodeType);

            return $"({left} {op} {right})";
        }

        private static string GetOperator(ExpressionType type) => type switch
        {
            ExpressionType.Equal => "eq",
            ExpressionType.NotEqual => "ne",
            ExpressionType.GreaterThan => "gt",
            ExpressionType.GreaterThanOrEqual => "ge",
            ExpressionType.LessThan => "lt",
            ExpressionType.LessThanOrEqual => "le",
            ExpressionType.AndAlso => "and",
            ExpressionType.OrElse => "or",
            _ => throw new NotSupportedException($"Operator '{type}' is not supported.")
        };

        private static string VisitMethodCall(MethodCallExpression expr)
        {
            if (expr.Object == null || expr.Arguments.Count != 1)
                throw new NotSupportedException("Only instance methods with one argument are supported.");

            string target = Visit(expr.Object);
            string argument = Visit(expr.Arguments[0]);

            return expr.Method.Name switch
            {
                "Contains" => $"contains({target}, {argument})",
                "StartsWith" => $"startswith({target}, {argument})",
                "EndsWith" => $"endswith({target}, {argument})",
                _ => throw new NotSupportedException($"Method '{expr.Method.Name}' is not supported.")
            };
        }

        private static string VisitMember(MemberExpression expr)
        {
            // If the expression is a captured constant (e.g., from closure), extract the value
            if (expr.Expression is ConstantExpression constExpr)
            {
                var container = constExpr.Value;
                var member = expr.Member;

                return FormatConstant(GetMemberValue(container, member));
            }

            // Nullable types: get the actual member
            if (expr.Member.Name == "Value" && Nullable.GetUnderlyingType(expr.Type) != null)
            {
                return Visit(expr.Expression!);
            }

            return expr.Member.Name;
        }

        private static object? GetMemberValue(object? obj, MemberInfo member)
        {
            return member switch
            {
                PropertyInfo prop => prop.GetValue(obj),
                FieldInfo field => field.GetValue(obj),
                _ => throw new NotSupportedException($"Member type '{member.MemberType}' not supported.")
            };
        }

        private static string VisitUnary(UnaryExpression expr)
        {
            // Handle conversions or parentheses
            if (expr.NodeType == ExpressionType.Convert)
                return Visit(expr.Operand);

            // Handle ! (not) operator
            if (expr.NodeType == ExpressionType.Not)
            {
                string operand = Visit(expr.Operand);
                return $"not ({operand})";
            }

            throw new NotSupportedException($"Unary expression '{expr.NodeType}' is not supported.");
        }

        private static string FormatConstant(object? value)
        {
            return value switch
            {
                null => "null",
                string s => $"'{s}'",
                bool b => b.ToString().ToLower(),
                DateTime dt => $"'{dt:yyyy-MM-ddTHH:mm:ss}'",
                Enum e => $"'{e}'",
                _ => value.ToString()!
            };
        }
    }
}

