using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Services;

namespace My.CoachManager.Domain.CategoryModule.Services
{
    public class CategoryDomainService : ReferenceDomainService<Category>, ICategoryDomainService
    {
        #region Fields
        
        private readonly IRepository<Player> _playerRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryDomainService"/>.
        /// </summary>
        public CategoryDomainService(IRepository<Category> categoryRepository, IRepository<Player> playerRepository) : base(categoryRepository)
        {
            _playerRepository = playerRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if the category can be removed.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool CanBeRemoved(Category category)
        {
            return !IsUsed(category.Id);
        }

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool IsUsed(int id)
        {
            return _playerRepository.Any(x => x.CategoryId == id);
        }

        #endregion Methods
    }
}