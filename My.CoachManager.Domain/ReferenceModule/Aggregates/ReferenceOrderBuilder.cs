using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.ReferenceModule.Aggregates
{
    public static class ReferenceOrderBuilder
    {
        /// <summary>
        /// Creates the order builder.
        /// </summary>
        public static QueryOrder<TEntity> OrderByLabel<TEntity>(bool descending = false) where TEntity : class, IEntity, IReference
        {
            var order = new QueryOrder<TEntity>();

            if (descending)
                order.OrderByDescending(x => x.Label);
            else
                order.OrderBy(x => x.Label);
            return order;
        }

        /// <summary>
        /// Creates the order builder.
        /// </summary>
        public static QueryOrder<TEntity> OrderByOrder<TEntity>(bool descending = false) where TEntity : class, IEntity, IOrderable
        {
            var order = new QueryOrder<TEntity>();
            if (descending)
                order.OrderByDescending(x => x.Order);
            else
                order.OrderBy(x => x.Order);
            return order;
        }
    }
}