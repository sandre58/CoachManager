using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;

namespace My.CoachManager.Presentation.Prism.Administration.ViewModels
{
    public class CategoryEditViewModel : EditViewModel<CategoryViewModel>, ICategoryEditViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryEditViewModel"/>.
        /// </summary>
        public CategoryEditViewModel(IAdminService adminService, IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            _adminService = adminService;
            Title = AdministrationResources.CategoryTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Save.
        /// </summary>
        protected override void SaveItemCore()
        {
            Item = _adminService.CreateOrUpdateCategory(Item.ToDto<CategoryDto>()).ToViewModel<CategoryViewModel>();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override CategoryViewModel LoadItemCore(int id)
        {
            return _adminService.GetCategoryById(id).ToViewModel<CategoryViewModel>();
        }

        #endregion Methods
    }
}