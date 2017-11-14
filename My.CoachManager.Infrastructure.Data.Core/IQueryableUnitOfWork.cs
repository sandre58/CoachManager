using System.Data.Entity;
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
        /// <returns></returns>
        IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class;

        /// <summary>
        /// Attach this item in ObjectStateManager
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        void Attach<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Set Object as Modified
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="item"></param>
        void SetModified<TEntity>(TEntity item) where TEntity : class;

        /// <summary>
        /// Apply Current Value in <paramref name="original"/>
        /// </summary>
        /// <typeparam name="TEntity">The type of Entity</typeparam>
        /// <param name="original">original entity</param>
        /// <param name="current">the current entity</param>
        void ApplyCurrentValues<TEntity>(TEntity original, TEntity current) where TEntity : class;

        void Lock<TEntity>(TEntity entity) where TEntity : class;

        void AddOrUpdate<T>(T entity, params string[] ignoreProperties) where T : class, IEntity;
    }
}