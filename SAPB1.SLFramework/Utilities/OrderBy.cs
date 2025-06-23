using System.Linq.Expressions;

namespace SAPB1.SLFramework.Utilities
{
    public static class OrderBy
    {
        public static (Expression<Func<T, object>> expr, bool desc) Asc<T>(Expression<Func<T, object>> expr)
            => (expr, false);

        public static (Expression<Func<T, object>> expr, bool desc) Desc<T>(Expression<Func<T, object>> expr)
            => (expr, true);
    }
}
