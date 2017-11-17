using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Admin.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.Person.Aggregate;

namespace My.CoachManager.Domain.Admin.Service
{
    public class CategoryDomainService : DomainService, ICategoryDomainService
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;
        private readonly IPlayerRepository _playerRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="categoryRepository"></param>
        public CategoryDomainService(ILogger logger, ICategoryRepository categoryRepository, IPlayerRepository playerRepository)
            : base(logger)
        {
            _categoryRepository = categoryRepository;
            _playerRepository = playerRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if category is unique.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool IsUnique(Category category)
        {
            return !_categoryRepository.Any(DataSpecification.IsUnique(category));
        }

        /// <summary>
        /// Check if the category can be removed.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool CanBeRemoved(Category category)
        {
            return !IsUsed(category);
        }

        /// <summary>
        /// Check if the category is used by others properties.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool IsUsed(Category category)
        {
            return _playerRepository.Any(x => x.CategoryId == category.Id);
        }

        #endregion Methods
    }
}