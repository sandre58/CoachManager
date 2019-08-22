using System.Collections.Generic;
using System.Linq;

using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Exceptions;
using My.CoachManager.Domain.AppModule.Services;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.CategoryModule.Services;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregate;

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
        public int SaveCategory(CategoryDto dto)
        {
            return _crudDomainService.Save(dto, CategoryFactory.CreateEntity, CategoryFactory.UpdateEntity, x => _categoryDomainService.Validate(x));
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveCategory(int id)
        {
            if (_categoryDomainService.IsUsed(id))
            {
                throw new IsUsedException(GetLabel(id));
            }

            _crudDomainService.Remove(id);
        }

        /// <inheritdoc />
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
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        private string GetLabel(int id)
        {
            return _categoryRepository.Query.Where(x => x.Id == id).Select(x => x.Label).FirstOrDefault();
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<CategoryDto> GetCategories()
        {
            return _categoryRepository.GetAll(CategorySelectBuilder.SelectCategories(), ReferenceOrderBuilder.OrderByOrder<Category>()).ToList();
        }

        /// <inheritdoc />
        /// <summary>
        /// Update items Orders.
        /// </summary>
        /// <param name="values"></param>
        public void UpdateOrders(IDictionary<int, int> values)
        {
            var entities = _categoryRepository.GetByFilter(x => values.Keys.Contains(x.Id), ReferenceOrderBuilder.OrderByOrder<Category>()).ToList();

            _categoryDomainService.UpdateOrders(entities.ToDictionary(x => x, x => values[x.Id]));
        }

        #endregion Methods
    }
}
