using System.Linq.Expressions;

namespace SAPB1.SLFramework.Utilities
{
    public static class ODataSelectBuilder
    {
        public static string ToODataSelect<TSource, TTarget>(Expression<Func<TSource, TTarget>> selector)
        {
            return selector.Body switch
            {
                MemberInitExpression initExpr => GetMembersFromMemberInit(initExpr),
                NewExpression newExpr => GetMembersFromNew(newExpr),
                MemberExpression memberExpr => GetFullMemberPath(memberExpr),
                _ => throw new NotSupportedException("Unsupported select expression. Use an anonymous type, DTO, or single property.")
            };
        }

        private static string GetMembersFromMemberInit(MemberInitExpression expr)
        {
            var props = expr.Bindings
                .OfType<MemberAssignment>()
                .Select(b => GetFullMemberPath(b.Expression))
                .Distinct();

            return string.Join(",", props);
        }

        private static string GetMembersFromNew(NewExpression expr)
        {
            var props = expr.Arguments
                .OfType<Expression>()
                .Select(GetFullMemberPath)
                .Distinct();

            return string.Join(",", props);
        }

        private static string GetFullMemberPath(Expression expr)
        {
            if (expr is UnaryExpression unary && unary.Operand is MemberExpression memberOperand)
                expr = memberOperand;

            if (expr is not MemberExpression member)
                throw new NotSupportedException($"Only member access is supported in projections. Got: {expr}");

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
