using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregate;

namespace My.CoachManager.Domain.CategoryModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class CategoryValidator : ReferenceValidator<Category>
    {
        
        /// <summary>
        /// Initialise a new instance of <see cref="ReferenceValidator{TEntity}"/>.
        /// </summary>
        public CategoryValidator(IRepository<Category> repository) : base(repository)
        {
        }

    }
}
