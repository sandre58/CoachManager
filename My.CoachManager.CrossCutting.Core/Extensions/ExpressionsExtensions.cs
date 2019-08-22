using System;
using System.Linq.Expressions;
using System.Reflection;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class ExpressionsExtensions
    {
        public static LambdaExpression ExpressionFromString<T>(string propName)
        {
            LambdaExpression func = null;

            var type = typeof(T);
            var prop = type.GetProperty(propName);
            ParameterExpression tpe = Expression.Parameter(typeof(T));
            if (prop != null)
            {
                Expression left = Expression.Property(tpe, prop);
                func = Expression.Lambda(left, tpe);
            }

            return func;
        }

        public static Expression<Func<T, bool>> ExpressionFromString<T>(string propName, string opr, string value, Expression<Func<T, bool>> expr = null)
        {
            Expression<Func<T, bool>> func = null;

            var type = typeof(T);
            var prop = type.GetProperty(propName);
            ParameterExpression tpe = Expression.Parameter(typeof(T));
            if (prop != null)
            {
                Expression left = Expression.Property(tpe, prop);
                Expression right = Expression.Convert(ToExprConstant(prop, value), prop.PropertyType);
                Expression<Func<T, bool>> innerExpr = Expression.Lambda<Func<T, bool>>(ApplyFilter(opr, left, right), tpe);
                if (expr != null)
                    innerExpr = innerExpr.And(expr);
                func = innerExpr;
            }

            return func;
        }

        private static Expression ToExprConstant(PropertyInfo prop, string value)
        {
            object val;

            switch (prop.Name)
            {
                case "System.Guid":
                    val = Guid.NewGuid();
                    break;

                default:
                    {
                        val = Convert.ChangeType(value, prop.PropertyType);
                        break;
                    }
            }

            return Expression.Constant(val);
        }

        private static BinaryExpression ApplyFilter(string opr, Expression left, Expression right)
        {
            BinaryExpression innerLambda = null;
            switch (opr)
            {
                case "==":
                case "=":
                    innerLambda = Expression.Equal(left, right);
                    break;

                case "<":
                    innerLambda = Expression.LessThan(left, right);
                    break;

                case ">":
                    innerLambda = Expression.GreaterThan(left, right);
                    break;

                case ">=":
                    innerLambda = Expression.GreaterThanOrEqual(left, right);
                    break;

                case "<=":
                    innerLambda = Expression.LessThanOrEqual(left, right);
                    break;

                case "!=":
                    innerLambda = Expression.NotEqual(left, right);
                    break;

                case "&&":
                    innerLambda = Expression.And(left, right);
                    break;

                case "||":
                    innerLambda = Expression.Or(left, right);
                    break;
            }
            return innerLambda;
        }

        public static Expression<Func<T, TResult>> And<T, TResult>(this Expression<Func<T, TResult>> expr1, Expression<Func<T, TResult>> expr2)
        {
            var invokedExpr = Expression.Invoke(expr2, expr1.Parameters);
            return Expression.Lambda<Func<T, TResult>>(Expression.AndAlso(expr1.Body, invokedExpr), expr1.Parameters);
        }

        public static Func<T, TResult> ExpressionToFunc<T, TResult>(this Expression<Func<T, TResult>> expr)
        {
            var res = expr.Compile();
            return res;
        }
    }
}