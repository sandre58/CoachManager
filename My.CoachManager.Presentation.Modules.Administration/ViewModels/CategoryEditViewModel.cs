using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    public class CategoryEditViewModel : EditViewModel<CategoryModel>
    {
        #region Fields

        private readonly ICategoryService _categoryService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryEditViewModel"/>.
        /// </summary>
        public CategoryEditViewModel(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion Constructors

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return _categoryService.SaveCategory(CategoryFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override CategoryModel LoadItemCore(int id)
        {
            return CategoryFactory.Get(_categoryService.GetCategoryById(id));
        }

        #endregion Methods
    }
}