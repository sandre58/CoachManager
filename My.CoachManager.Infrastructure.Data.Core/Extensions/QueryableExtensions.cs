using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Domain.Core;
using System.Data.Entity.Core.Objects;

namespace My.CoachManager.Infrastructure.Data.Core.Extensions
{
    /// <summary>
    /// Extension class for Query interface object.
    /// </summary>
    public static class QueryableExtensions
    {
        /// <summary>
        /// Include for Navigation Properties.
        /// </summary>
        /// <typeparam name="T">Entity Type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="includes">The includes.</param>
        /// <returns>Query where added include.</returns>
        public static IQueryable<T> Include<T>(this IQueryable<T> source, IEnumerable<Expression<Func<T, object>>> includes)
            where T : class, IEntity
        {
            return source == null ? null : includes.Aggregate(source, (current, include) => current.Include(include));
        }

        /// <summary>
        /// Applies the query order.
        /// </summary>
        /// <typeparam name="T">Entity Type.</typeparam>
        /// <param name="source">The source.</param>
        /// <param name="order">The order.</param>
        /// <returns>Query where order added.</returns>
        public static IQueryable<T> ApplyQueryOrder<T>(this IQueryable<T> source, QueryOrder<T> order)
            where T : class, IEntity
        {
            source = order.GetOrderByList.Aggregate(source, (current, item) => Queryable.OrderBy(current, (dynamic)item));

            source = order.GetOrderByDescendingList.Aggregate(source, (current, item) => Queryable.OrderByDescending(current, (dynamic)item));

            source = order.GetThenByList.Aggregate(source, (current, item) => Queryable.ThenBy((IOrderedQueryable<T>)current, (dynamic)item));

            return order.GetThenByDescendingList.Aggregate(source, (current, item) => Queryable.ThenByDescending((IOrderedQueryable<T>)current, (dynamic)item));
        }

        /// <summary>
        /// Get SQL Query of the IQUERYABLE.
        /// </summary>
        /// <typeparam name="T">Entity Type.</typeparam>
        /// <param name="query">Query to Trace.</param>
        /// <returns>SQL result.</returns>
        public static string TraceSqlQuery<T>(this IQueryable<T> query)
        {
            var internalQueryField = query.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name.Equals("_internalQuery") || f.Name.Equals("_internalSet"));

            if (internalQueryField == null)
            {
                return SqlConstants.QueryNotShowable;
            }

            var internalQuery = internalQueryField.GetValue(query);

            var objectQueryField = internalQuery.GetType().GetFields(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(f => f.Name.Equals("_objectQuery") || f.Name.Equals("_localView"));

            if (objectQueryField == null)
            {
                return SqlConstants.QueryNotShowable;
            }

            // If the Iqueryable object have no parameter, juste print the IQueryable.ToString() value.
            if (!(objectQueryField.GetValue(internalQuery) is ObjectQuery<T> objectQuery))
            {
                return internalQuery.ToString().Replace("\r\n    ", string.Empty);
            }

            // Call Methode to return SQL string with values of parameters
            return TraceSqlQueryWithParameters(objectQuery);
        }

        /// <summary>
        /// Create SQL string from ObjectQuery.
        /// </summary>
        /// <param name="objectQuery">Query to Trace.</param>
        /// <returns>SQL string.</returns>
        private static string TraceSqlQueryWithParameters(ObjectQuery objectQuery)
        {
            var sb = new StringBuilder();
            sb.Append("Query : ");
            sb.Append(objectQuery.ToTraceString().Replace("\r\n    ", string.Empty));

            if (objectQuery.Parameters.Any())
            {
                foreach (var p in objectQuery.Parameters)
                {
                    string value;

                    if (p.ParameterType == typeof(string))
                    {
                        value = p.Value == null ? "Null" : String.Concat("'", p.Value.ToString().Replace("~", string.Empty), "'");

                        var replacedValue = String.Concat("@", p.Name, " ESCAPE '~'");

                        sb.Replace(replacedValue, value);
                    }
                    else
                    {
                        value = p.Value?.ToString() ?? "Null";
                    }

                    sb.Replace("@" + p.Name + ' ', value + ' ');
                    sb.Replace("@" + p.Name + ')', value + ')');
                }
            }

            return sb.ToString();
        }
    }
}