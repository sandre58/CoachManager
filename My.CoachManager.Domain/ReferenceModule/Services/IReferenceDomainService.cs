using System.Collections.Generic;
using FluentValidation.Results;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.ReferenceModule.Services
{
    public interface IReferenceDomainService<TEntity>
        where TEntity : class, IReference, new()
    {
        /// <summary>
        /// Check if TEntity is unique.
        /// </summary>
        /// <returns></returns>
        bool IsUnique(TEntity entity);

        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="values"></param>
        void UpdateOrders(IDictionary<TEntity, int> values);

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
       ValidationResult Validate(TEntity entity);
    }
}