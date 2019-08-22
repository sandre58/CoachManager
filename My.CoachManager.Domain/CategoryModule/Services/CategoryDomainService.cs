using FluentValidation.Results;

using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Services;

namespace My.CoachManager.Domain.CategoryModule.Services
{
    public class CategoryDomainService : ReferenceDomainService<Category>, ICategoryDomainService
    {
        #region Fields
        
        private readonly IRepository<Roster> _rosterRepository;
        private readonly IRepository<RosterPlayer> _rosterPlayerRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryDomainService"/>.
        /// </summary>
        public CategoryDomainService(IRepository<Category> categoryRepository, IRepository<RosterPlayer> rosterPlayerRepository, IRepository<Roster> rosterRepository) : base(categoryRepository)
        {
            _rosterPlayerRepository = rosterPlayerRepository;
            _rosterRepository = rosterRepository;
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Check if the category can be removed.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool CanBeRemoved(Category category)
        {
            return !IsUsed(category.Id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUsed(int id)
        {
            return _rosterRepository.Any(x => x.CategoryId == id) || 
                   _rosterPlayerRepository.Any(x => x.CategoryId.HasValue && x.CategoryId.Value == id);
        }

        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public override ValidationResult Validate(Category entity)
        {
            var validator = new CategoryValidator(Repository);
            return validator.Validate(entity);
        }

        #endregion Methods
    }
}
