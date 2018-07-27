using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Domain.ReferenceModule.Service
{
    public class ReferenceDomainService<TEntity> : IReferenceDomainService<TEntity>
        where TEntity : class, IReference, new()
    {
        #region Fields

        private readonly IRepository<TEntity> _repository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ReferenceDomainService{T}"/>.
        /// </summary>
        /// <param name="repository"></param>
        public ReferenceDomainService(IRepository<TEntity> repository)
        {
            _repository = repository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if TEntity is unique.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public bool IsUnique(TEntity entity)
        {
            return !_repository.Any(ReferenceSpecification.IsUnique(entity));
        }

        /// <summary>
        /// Update Order.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="newOrder"></param>
        /// <returns></returns>
        public void UpdateOrder(TEntity entity, int newOrder)
        {
            entity.Order = newOrder;
            _repository.Modify(entity);
            _repository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}