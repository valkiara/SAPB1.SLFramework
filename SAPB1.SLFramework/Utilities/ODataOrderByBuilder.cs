using System.Linq.Expressions;

namespace SAPB1.SLFramework.Utilities
{
    public static class ODataOrderByBuilder
    {
        public static string ToODataOrderBy<T>(IEnumerable<(Expression<Func<T, object>> expr, bool desc)> fields)
        {
            return string.Join(",", fields.Select(f => BuildField(f.expr, f.desc)));
        }

        private static string BuildField<T>(Expression<Func<T, object>> expr, bool desc)
        {
            string field = ExtractMemberName(expr.Body);
            return desc ? $"{field} desc" : field;
        }

        private static string ExtractMemberName(Expression expression)
        {
            if (expression is UnaryExpression unary && unary.Operand is MemberExpression unaryMember)
                return GetFullMemberPath(unaryMember);

            if (expression is MemberExpression member)
                return GetFullMemberPath(member);

            throw new NotSupportedException("Only direct property access is supported in OrderBy.");
        }

        private static string GetFullMemberPath(MemberExpression member)
        {
            var stack = new Stack<string>();
            while (member != null)
            {
                stack.Push(member.Member.Name);
                member = member.Expression as MemberExpression;
            }
            return string.Join("/", stack);
        }
    }

}
