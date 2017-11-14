using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Core;
using My.CoachManager.Infrastructure.Data.Core.Resources;

namespace My.CoachManager.Infrastructure.Data.Core
{
    /// <summary>
    /// Repository Generic.
    /// </summary>
    /// <typeparam name="TEntity">Type Of Entity.</typeparam>
    public class GenericRepository<TEntity> : BaseRepository<TEntity>, IGenericRepository<TEntity>
        where TEntity : class, IEntity, new()
    {
        #region ----- Constructor -----

        /// <summary>
        /// Initializes a new instance of the <see cref="GenericRepository{TEntity}"/> class.
        /// </summary>
        /// <param name="unitOfWork">A unit of work for this repository.</param>
        /// <param name="logger">Logger dependency.</param>
        public GenericRepository(IQueryableUnitOfWork unitOfWork, ILogger logger)
            : base(unitOfWork, logger)
        {
        }

        #endregion ----- Constructor -----

        #region ----- Get Methods -----

        /// <summary>
        /// Get Element by it's ID.
        /// </summary>
        /// <param name="id">Primary Key.</param>
        /// <param name="includes">Includes Parameters.</param>
        /// <returns>Single or Default TEntity.</returns>
        public virtual TEntity GetEntity(int id, params Expression<Func<TEntity, object>>[] includes)
        {
            Logger.Debug(string.Format(CultureInfo.InvariantCulture, TraceResources.Trace_GetEntityByID, typeof(TEntity).Name, id));

            return GetFilteredElements<int, TEntity>(x => x, x => x.Id.Equals(id), null, true, 0, 0, false, null, includes).Item1.SingleOrDefault();
        }

        #endregion ----- Get Methods -----
    }
}