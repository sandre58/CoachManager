using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.ReferenceModule.Service
{
    public interface IReferenceDomainService<TEntity>
        where TEntity : class, IReference, new()
    {
        /// <summary>
        /// Check if TEntity is unique.
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        bool IsUnique(TEntity item);

        /// <summary>
        /// Update Order.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        void UpdateOrder(TEntity entity, int newOrder);
    }
}