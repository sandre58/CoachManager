using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Query Order.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entity.</typeparam>
    public class QueryOrder<TEntity>
        where TEntity : class, IEntityBase
    {
        /// <summary>
        /// The order by descending list.
        /// </summary>
        private readonly IList<LambdaExpression> _orderByDescendingList;

        /// <summary>
        /// The order by list.
        /// </summary>
        private readonly IList<LambdaExpression> _orderByList;

        /// <summary>
        /// The then by descending list.
        /// </summary>
        private readonly IList<LambdaExpression> _thenByDescendingList;

        /// <summary>
        /// The then by list.
        /// </summary>
        private readonly IList<LambdaExpression> _thenByList;

        /// <summary>
        /// Initializes a new instance of the <see cref="QueryOrder{TEntity}"/> class.
        /// </summary>
        public QueryOrder()
        {
            _orderByList = new List<LambdaExpression>();
            _orderByDescendingList = new List<LambdaExpression>();
            _thenByList = new List<LambdaExpression>();
            _thenByDescendingList = new List<LambdaExpression>();
        }

        /// <summary>
        /// Gets the order by descending list.
        /// </summary>
        /// <returns>List of order.</returns>
        public IList<LambdaExpression> GetOrderByDescendingList
        {
            get
            {
                return _orderByDescendingList;
            }
        }

        /// <summary>
        /// Gets the order by list.
        /// </summary>
        /// <returns>List of order.</returns>
        public IList<LambdaExpression> GetOrderByList
        {
            get
            {
                return _orderByList;
            }
        }

        /// <summary>
        /// Gets the then by descending list.
        /// </summary>
        /// <returns>List of order.</returns>
        public IList<LambdaExpression> GetThenByDescendingList
        {
            get
            {
                return _thenByDescendingList;
            }
        }

        /// <summary>
        /// Gets the then by list.
        /// </summary>
        /// <returns>List of order.</returns>
        public IList<LambdaExpression> GetThenByList
        {
            get
            {
                return _thenByList;
            }
        }

        /// <summary>
        /// Set the order.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>
        /// Current class object.
        /// </returns>
        public QueryOrder<TEntity> OrderBy<TKey>(Expression<Func<TEntity, TKey>> orderExpression)
        {
            // Agregate
            _orderByList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the order.
        /// </summary>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>Current class object.</returns>
        public QueryOrder<TEntity> OrderBy(LambdaExpression orderExpression)
        {
            // Agregate
            _orderByList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the descending order.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>
        /// Current class object.
        /// </returns>
        public QueryOrder<TEntity> OrderByDescending<TKey>(Expression<Func<TEntity, TKey>> orderExpression)
        {
            // Agregate
            _orderByDescendingList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the descending order.
        /// </summary>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>Current class object.</returns>
        public QueryOrder<TEntity> OrderByDescending(LambdaExpression orderExpression)
        {
            // Agregate
            _orderByDescendingList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the secondary order.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>
        /// Current class object.
        /// </returns>
        public QueryOrder<TEntity> ThenBy<TKey>(Expression<Func<TEntity, TKey>> orderExpression)
        {
            // Agregate
            _thenByList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the secondary order.
        /// </summary>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>Current class object.</returns>
        public QueryOrder<TEntity> ThenBy(LambdaExpression orderExpression)
        {
            // Agregate
            _thenByList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the secondary descending order.
        /// </summary>
        /// <typeparam name="TKey">The type of the key.</typeparam>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>
        /// Current class object.
        /// </returns>
        public QueryOrder<TEntity> ThenByDescending<TKey>(Expression<Func<TEntity, TKey>> orderExpression)
        {
            // Agregate
            _thenByDescendingList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Set the secondary descending order.
        /// </summary>
        /// <param name="orderExpression">The order expression.</param>
        /// <returns>Current class object.</returns>
        public QueryOrder<TEntity> ThenByDescending(LambdaExpression orderExpression)
        {
            // Agregate
            _thenByDescendingList.Add(orderExpression);

            return this;
        }

        /// <summary>
        /// Writes the information.
        /// </summary>
        /// <returns>List of order.</returns>
        public string WriteInfo()
        {
            var info = new StringBuilder();

            info.Append("OrderBy : ");
            info.Append(string.Join(",", GetMember(_orderByList)));

            info.Append(" - OrderByDescending : ");
            info.Append(string.Join(",", GetMember(_orderByDescendingList)));

            info.Append(" - ThenBy : ");
            info.Append(string.Join(",", GetMember(_thenByList)));

            info.Append(" - ThenByDescending : ");
            info.Append(string.Join(",", GetMember(_thenByDescendingList)));

            return info.ToString();
        }

        /// <summary>
        /// Gets the member.
        /// </summary>
        /// <param name="expressions">The expressions.</param>
        /// <returns>List of member name.</returns>
        private static IEnumerable<string> GetMember(IEnumerable<LambdaExpression> expressions)
        {
            return expressions.Select(item => item.Body).OfType<MemberExpression>().Select(memberEx => memberEx.ToString()).ToList();
        }
    }
}