using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Reflection;

namespace SAPB1.SLFramework.Utilities
{
    public static class ODataSelectBuilder
    {
        // Use this when projecting T -> T (no ambiguity)
        public static string ToODataSelectSelf<T>(Expression<Func<T, T>> selector)
            => ToODataSelect<T, T>(selector);

        // General form: TSource -> TTarget
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

        /// <summary>
        /// Returns a minimal $select for key-like fields of T.
        /// </summary>
        public static string MinimalKeySelect<T>()
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance);

            // 1) [Key] attribute(s)
            var keyAttrs = props.Where(p => p.GetCustomAttribute<KeyAttribute>() != null).ToArray();
            if (keyAttrs.Length > 0)
                return string.Join(",", keyAttrs.Select(p => p.Name));

            // 2) Common B1 keys
            string[] priorityNames = { "DocEntry", "AbsEntry", "Code", "DocNum" };
            var hits = props.Where(p => priorityNames.Contains(p.Name, StringComparer.OrdinalIgnoreCase)).ToList();

            if (hits.Count == 0)
            {
                var suffixHits = props.Where(p =>
                       p.Name.EndsWith("Code", StringComparison.OrdinalIgnoreCase)
                    || p.Name.EndsWith("Entry", StringComparison.OrdinalIgnoreCase)).ToList();
                if (suffixHits.Count > 0) hits.AddRange(suffixHits);
            }

            if (hits.Count > 0)
                return string.Join(",", hits.Select(p => p.Name).Distinct());

            // 3) Fallback: a simple property
            var simple = props.FirstOrDefault(p =>
                    p.PropertyType.IsPrimitive
                    || p.PropertyType == typeof(string)
                    || p.PropertyType == typeof(Guid)
                    || p.PropertyType == typeof(DateTime)
                    || p.PropertyType == typeof(decimal)
                    || p.PropertyType == typeof(long)
                    || p.PropertyType == typeof(int));

            return simple?.Name ?? props.First().Name;
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
