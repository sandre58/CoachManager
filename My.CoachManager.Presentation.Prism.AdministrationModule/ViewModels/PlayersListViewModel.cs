using System.Collections.ObjectModel;
using My.CoachManager.Application.Dtos.Persons;
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
    public class PlayersListViewModel : ListViewModel<PlayerViewModel, PlayerEditView, PlayerEditViewModel>, IPlayersListViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoriesListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IAdminService adminService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            _adminService = adminService;

            Title = AdministrationResources.PlayersTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(PlayerViewModel item)
        {
            _adminService.RemovePlayer(item.ToDto<PlayerDto>());
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore(bool isFirstLoading = false)
        {
            var result = _adminService.GetPlayersList();

            Items = new ObservableCollection<PlayerViewModel>(result.ToViewModels<PlayerViewModel>());
        }

        #endregion Methods
    }
}