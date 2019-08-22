using Microsoft.EntityFrameworkCore;

using My.CoachManager.Domain.Core;

namespace My.CoachManager.Infrastructure.Data.Core
{
    /// <summary>
    /// Interface for Launch Queries on Context.
    /// </summary>
    public interface IQueryableUnitOfWork : IUnitOfWork
    {
        /// <summary>
        /// Returns an IObjectSet for access to entities of the given type in the context,
        /// the ObjectStateManager, and the underlying store.
        /// </summary>
        /// <typeparam name="TEntity">an IObjectWithChangeTracker Entity</typeparam>
        /// <returns>The database set of entities.</returns>
        DbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// Attach this item in ObjectStateManager
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="item">Item to attach in context.</param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Set Object as Modified
        /// </summary>
        /// <typeparam name="TEntity">Type of the entity.</typeparam>
        /// <param name="item">Item to set modified in context.</param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

    }
}
