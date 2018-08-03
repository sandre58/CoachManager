using System.Collections.Generic;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Domain.ReferenceModule.Services
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

        /// <inheritdoc />
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
        /// Update items Orders.
        /// </summary>
        /// <param name="values"></param>
        public void UpdateOrders(IDictionary<TEntity, int> values)
        {
            foreach (var value in values)
            {
                value.Key.Order = value.Value;
                _repository.Modify(value.Key);
            }
            _repository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}