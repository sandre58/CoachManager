using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.Admin.Aggregate;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.Admin.Service
{
    public class CategoryDomainService : DomainService, ICategoryDomainService
    {
        #region Fields

        private readonly ICategoryRepository _categoryRepository;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryDomainService"/>.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="categoryRepository"></param>
        public CategoryDomainService(ILogger logger, ICategoryRepository categoryRepository)
            : base(logger)
        {
            _categoryRepository = categoryRepository;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Check if category is unique.
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public bool CheckCategoryIsUnique(Category category)
        {
            return !_categoryRepository.Any(DataSpecification.IsUnique(category));
        }

        #endregion Methods
    }
}