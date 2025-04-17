using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace GF.Graph
{
    public static partial class Helpers
    {
        public static class FuncTest
        {
            public static bool FuncEqual<TSource, TValue>(
                Expression<Func<TSource, TValue>> x,
                Expression<Func<TSource, TValue>> y)
            {
                return ExpressionEqual(x, y);
            }
            private static bool ExpressionEqual(Expression x, Expression y)
            {
                // deal with the simple cases first...
                if (ReferenceEquals(x, y)) return true;
                if (x == null || y == null) return false;
                if (x.NodeType != y.NodeType
                    || x.Type != y.Type) return false;

                switch (x.NodeType)
                {
                    case ExpressionType.Lambda:
                        return ExpressionEqual(((LambdaExpression)x).Body, ((LambdaExpression)y).Body);
                    case ExpressionType.NotEqual:
                        return x == y;
                    case ExpressionType.MemberAccess:
                        MemberExpression mex = (MemberExpression)x, mey = (MemberExpression)y;
                        return mex.Member == mey.Member; // should really test down-stream expression
                    default:
                        throw new NotImplementedException(x.NodeType.ToString());
                }
            }
        }
    }
}
