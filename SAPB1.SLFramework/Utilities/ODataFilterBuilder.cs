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

        private static string VisitBinary(BinaryExpression binary)
        {
            var left = Visit(binary.Left);

            string right;

            // Only evaluate the right if it does NOT depend on the parameter
            if (IsParameterDependent(binary.Right))
            {
                right = Visit(binary.Right);
            }
            else
            {
                try
                {
                    var rightValue = EvaluateExpression(binary.Right);
                    string? propertyName = GetPropertyName(binary.Left);
                    right = FormatConstant(rightValue, propertyName);
                }
                catch
                {
                    right = Visit(binary.Right);
                }
            }

            var op = GetOperator(binary.NodeType);
            return $"({left} {op} {right})";
        }

        private static string? GetPropertyName(Expression expr)
        {
            if (expr is MemberExpression memberExpr)
                return memberExpr.Member.Name;

            if (expr is UnaryExpression unaryExpr)
                return GetPropertyName(unaryExpr.Operand);

            return null;
        }



        private static bool IsParameterDependent(Expression expr)
        {
            return expr switch
            {
                ParameterExpression => true,
                MemberExpression m => IsParameterDependent(m.Expression),
                UnaryExpression u => IsParameterDependent(u.Operand),
                _ => false
            };
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
            string argument;

            try
            {
                var val = EvaluateExpression(expr.Arguments[0]);
                argument = FormatConstant(val);
            }
            catch (NotSupportedException)
            {
                argument = Visit(expr.Arguments[0]);
            }

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
            if (expr.Expression is ParameterExpression)
            {
                return expr.Member.Name;
            }

            try
            {
                var value = EvaluateExpression(expr);
                return FormatConstant(value);
            }
            catch (Exception ex)
            {
                throw new NotSupportedException($"Unsupported member expression: {expr}", ex);
            }
        }

        private static string VisitUnary(UnaryExpression expr)
        {
            if (expr.NodeType == ExpressionType.Convert)
                return Visit(expr.Operand);

            if (expr.NodeType == ExpressionType.Not)
            {
                var operand = Visit(expr.Operand);
                return $"not ({operand})";
            }

            throw new NotSupportedException($"Unary expression '{expr.NodeType}' is not supported.");
        }

        private static object? EvaluateExpression(Expression expr)
        {
            switch (expr)
            {
                case ConstantExpression constant:
                    return constant.Value;

                case MemberExpression memberExpr:
                    if (memberExpr.Expression is ParameterExpression)
                        throw new NotSupportedException("Cannot evaluate parameter expression directly.");

                    var obj = EvaluateExpression(memberExpr.Expression!);
                    return memberExpr.Member switch
                    {
                        FieldInfo f => f.GetValue(obj),
                        PropertyInfo p => p.GetValue(obj),
                        _ => throw new NotSupportedException("Unsupported member type.")
                    };

                case UnaryExpression unary:
                    return EvaluateExpression(unary.Operand); // ⚠ preserves enum

                case ParameterExpression:
                    throw new NotSupportedException("Parameter expression cannot be evaluated.");

                default:
                    var lambda = Expression.Lambda(expr);
                    return lambda.Compile().DynamicInvoke(); // ⚠ this may box enums too, but usually keeps type
            }
        }



        private static string FormatConstant(object? value, string? propertyName = null)
        {
            if (value == null)
                return "null";

            var type = value.GetType();

            if (type.IsEnum)
            {
                if (!string.IsNullOrEmpty(propertyName) && propertyName.StartsWith("U_"))
                {
                    return Convert.ToInt32(value).ToString(); // for SAP UDFs
                }
                else
                {
                    return $"'{value}'"; // for enums like cCustomer
                }
            }

            return value switch
            {
                string s => $"'{s}'",
                bool b => b.ToString().ToLower(),
                DateTime dt => $"'{dt:yyyy-MM-ddTHH:mm:ss}'",
                _ => value.ToString()!
            };
        }

    }
}
