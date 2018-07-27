using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.CategoryModule.Service;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregates;

namespace My.CoachManager.Application.Services.CategoryModule
{
    /// <summary>
    /// Implementation of the ICategoryAppService class.
    /// </summary>
    public class CategoryAppService : ICategoryAppService
    {
        #region ---- Fields ----

        private readonly IRepository<Category> _categoryRepository;

        private readonly ICrudDomainService<Category, CategoryDto> _crudDomainService;

        private readonly ICategoryDomainService _categoryDomainService;

        #endregion ---- Fields ----

        #region ---- Constructors ----

        /// <summary>
        /// Initializes a new instance of the <see cref="CategoryAppService"/> class.
        /// </summary>
        /// <param name="categoryRepository"></param>
        /// <param name="crudDomainService"></param>
        /// <param name="categoryDomainService"></param>
        public CategoryAppService(IRepository<Category> categoryRepository, ICrudDomainService<Category, CategoryDto> crudDomainService, ICategoryDomainService categoryDomainService)
        {
            _categoryRepository = categoryRepository;
            _categoryDomainService = categoryDomainService;
            _crudDomainService = crudDomainService;
        }

        #endregion ---- Constructors ----

        #region Methods

        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public CategoryDto SaveCategory(CategoryDto dto)
        {
            if (!_categoryDomainService.IsUnique(CategoryFactory.CreateEntity(dto)))
            {
                throw new BusinessException(string.Format(ValidationMessageResources.AlreadyExistMessage, dto.Label));
            }

            return _crudDomainService.Save(dto, CategoryFactory.CreateEntity, CategoryFactory.UpdateEntity);
        }

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveCategory(CategoryDto dto)
        {
            if (_categoryDomainService.IsUsed(dto.Id))
            {
                throw new BusinessException(MessageResources.RemovingFailed + " " + string.Format(ValidationMessageResources.IsUsedMessage, dto.Label));
            }

            _crudDomainService.Remove(dto);
        }

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public CategoryDto GetCategoryById(int id)
        {
           var entity = _categoryRepository.GetEntity(id);
            return entity != null ? CategoryFactory.Get(entity) : null;
        }

        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryDto> GetCategories()
        {
            var items = _categoryRepository.GetAll(ReferenceOrderBuilder.OrderByOrder<Category>());
            return items.Select(CategoryFactory.Get).ToList();
        }

        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="values"></param>
        public void UpdateOrders(IDictionary<int, int> values)
        {
            var entities = _categoryRepository.GetByFilter(x => values.Keys.Contains(x.Id), new QueryOrder<Category>());
            foreach (var entity in entities)
            {
                _categoryDomainService.UpdateOrder(entity, values[entity.Id]);
            }
        }

        #endregion Methods
    }
}