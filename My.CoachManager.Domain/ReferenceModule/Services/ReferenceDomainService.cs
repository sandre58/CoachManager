using System.Collections.Generic;
using FluentValidation.Results;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Domain.ReferenceModule.Services
{
    public abstract class ReferenceDomainService<TEntity> : IReferenceDomainService<TEntity>
        where TEntity : class, IReference, new()
    {
        #region Fields

        protected readonly IRepository<TEntity> Repository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ReferenceDomainService{T}"/>.
        /// </summary>
        /// <param name="repository"></param>
        public ReferenceDomainService(IRepository<TEntity> repository)
        {
            Repository = repository;
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Check if TEntity is unique.
        /// </summary>
        /// <returns></returns>
        public bool IsUnique(TEntity entity)
        {
            return !Repository.Any(ReferenceSpecification.IsUnique(entity));
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
                Repository.Modify(value.Key);
            }
            Repository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public abstract ValidationResult Validate(TEntity entity);

        #endregion Methods
    }
}