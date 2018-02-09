using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain
{
    public static class LabelableOrderBuilder
    {
        /// <summary>
        /// Creates the order builder.
        /// </summary>
        public static QueryOrder<TEntity> OrderByLabel<TEntity>(bool descending = false) where TEntity : class, IEntityBase, ILabelable
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
        public static QueryOrder<TEntity> OrderByOrder<TEntity>(bool descending = false) where TEntity : class, IEntityBase, IOrderable
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