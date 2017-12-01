using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Core.Specification;

namespace My.CoachManager.Domain
{
    public static class EntitySpecification
    {
        /// <summary>
        /// Specification by id.
        /// </summary>
        public static ISpecification<TEntity> ById<TEntity>(int id)
            where TEntity : class, IEntity
        {
            Specification<TEntity> specification = new TrueSpecification<TEntity>();

            specification &= new DirectSpecification<TEntity>(x => x.Id == id);

            return specification;
        }
    }
}