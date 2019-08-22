using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Core.Specification;

namespace My.CoachManager.Domain.ReferenceModule.Aggregate
{
    public static class ReferenceSpecification
    {
        /// <summary>
        /// Specification exist in other customer.
        /// </summary>
        /// <returns>The specification for getting if exist in other customer.</returns>
        public static ISpecification<TEntity> IsUnique<TEntity>(TEntity entity)
            where TEntity : class, IEntity, IReference
        {
            Specification<TEntity> specification = new TrueSpecification<TEntity>();

            specification &= new DirectSpecification<TEntity>(x => x.Code == entity.Code);
            specification &= new DirectSpecification<TEntity>(x => x.Id != entity.Id);

            return specification;
        }
    }
}
