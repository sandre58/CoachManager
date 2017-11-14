using System.Collections.Generic;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provide an interface to request and manage a data source.
    /// </summary>
    /// <typeparam name="TEntity">A class.</typeparam>
    public interface IGenericRepository<TEntity> : IReadOnlyRepository<TEntity> where TEntity : class, IEntity, new()
    {
        #region ----- Add Methods -----

        /// <summary>
        /// Add an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Add(TEntity item);

        /// <summary>
        /// Add an Entities Object in Context.
        /// </summary>
        /// <param name="items">The items.</param>
        void AddRange(IEnumerable<TEntity> items);

        #endregion ----- Add Methods -----

        #region ----- Remove Methods -----

        /// <summary>
        /// Remove an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Remove(TEntity item);

        #endregion ----- Remove Methods -----

        #region ----- Modify Methods -----

        /// <summary>
        /// Modify an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void Modify(TEntity item);

        /// <summary>
        /// Modify a collection of Entity Objects in Context.
        /// </summary>
        /// <param name="items">Entity Object.</param>
        void Modify(ICollection<TEntity> items);

        #endregion ----- Modify Methods -----

        #region ----- Add Or Modify Methods -----

        /// <summary>
        /// Modify an Entity Object in Context.
        /// </summary>
        /// <param name="item">Entity Object.</param>
        void AddOrModify(TEntity item);

        /// <summary>
        /// Modify a collection of Entity Objects in Context.
        /// </summary>
        /// <param name="items">Entity Object.</param>
        void AddOrModify(ICollection<TEntity> items);

        #endregion ----- Add Or Modify Methods -----
    }
}