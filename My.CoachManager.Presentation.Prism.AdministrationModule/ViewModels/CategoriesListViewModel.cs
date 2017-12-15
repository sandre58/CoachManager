using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.AdministrationModule.Resources.Strings;
using My.CoachManager.Presentation.Prism.AdministrationModule.Views;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.AdministrationModule.ViewModels
{
    public class CategoriesListViewModel : OrderedListViewModel<CategoryViewModel, CategoryEditView, CategoryEditViewModel>, ICategoriesListViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoriesListViewModel"/>.
        /// </summary>
        public CategoriesListViewModel(IAdminService adminService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            _adminService = adminService;

            Title = AdministrationResources.CategoriesTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(CategoryViewModel item)
        {
            _adminService.RemoveCategory(item.ToDto<CategoryDto>());
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _adminService.UpdateCategoriesOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore(bool isFirstLoading = false)
        {
            var result = _adminService.GetCategoriesList();

            Items = new ObservableCollection<CategoryViewModel>(result.OrderBy(x => x.Order).ToViewModels<CategoryViewModel>());
        }

        #endregion Methods
    }
}