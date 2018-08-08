using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using My.CoachManager.Domain.Core;

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
    }
}