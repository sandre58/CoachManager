using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Core;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Application.Dtos.Mapping;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.CategoryModule.Service;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Services.Categories
{
    /// <summary>
    /// Implementation of the ICategoryAppService class.
    /// </summary>
    public class CategoryAppService : AppService, ICategoryAppService
    {
        #region ---- Fields ----

        private readonly ICategoryRepository _categoryRepository;

        private readonly ICategoryDomainService _categoryDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAppService"/> class.
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="categoryRepository"></param>
        /// <param name="categoryDomainService"></param>
        public CategoryAppService(ILogger logger, ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
            : base(logger)
        {
            _categoryRepository = categoryRepository;
            _categoryDomainService = categoryDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public CategoryDto CreateOrUpdate(CategoryDto dto)
        {
            var entity = dto.ToEntity<Category>();
            if (!_categoryDomainService.IsUnique(entity))
            {
                throw new BusinessException(string.Format(ValidationMessageResources.AlreadyExistMessage, entity.Label));
            }

            _categoryRepository.AddOrModify(entity);

            _categoryRepository.UnitOfWork.Commit();

            return entity.ToDto<CategoryDto>();
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void Remove(CategoryDto dto)
        {
            var entity = dto.ToEntity<Category>();

            if (_categoryDomainService.IsUsed(entity))
            {
                throw new BusinessException(MessageResources.RemovingFailed + " " + string.Format(ValidationMessageResources.IsUsedMessage, entity.Label));
            }

            _categoryRepository.Remove(entity);

            _categoryRepository.UnitOfWork.Commit();
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetById(int id)
        {
            return _categoryRepository.GetEntity(id).ToDto<CategoryDto>();
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetList()
        {
            return _categoryRepository.GetAll(CategorySelectBuilder.SelectCategoryForList()).ToArray();
        }

        /// <summary>
        /// Load all labels items.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<CategoryDto> GetLabels()
        {
            return _categoryRepository.GetAll(CategorySelectBuilder.SelectCategoryLabel()).ToArray();
        }

        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="entities"></param>
        public void UpdateOrders(IDictionary<int, int> entities)
        {
            foreach (var entity in entities)
            {
                _categoryRepository.GetEntity(entity.Key).Order = entity.Value;
            }

            _categoryRepository.UnitOfWork.Commit();
        }

        #endregion Methods
    }
}