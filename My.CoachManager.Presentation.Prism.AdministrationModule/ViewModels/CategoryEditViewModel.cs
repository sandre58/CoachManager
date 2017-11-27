using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.AdministrationModule.Resources.Strings;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.AdministrationModule.ViewModels
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
        public CategoryEditViewModel(IAdminService adminService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
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