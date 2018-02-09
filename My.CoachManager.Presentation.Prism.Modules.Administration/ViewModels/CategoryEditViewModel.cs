using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class CategoryEditViewModel : EditViewModel<CategoryViewModel>, ICategoryEditViewModel
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
            Title = AdministrationResources.CategoryTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            Item = _categoryService.CreateOrUpdate(Item.ToDto<CategoryDto>()).ToViewModel<CategoryViewModel>();
            return true;
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override CategoryViewModel LoadItemCore(int id)
        {
            return _categoryService.GetById(id).ToViewModel<CategoryViewModel>();
        }

        #endregion Methods
    }
}