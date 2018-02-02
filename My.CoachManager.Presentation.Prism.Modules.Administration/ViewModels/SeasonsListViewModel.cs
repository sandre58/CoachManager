using System;
using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class SeasonsListViewModel : OrderedListViewModel<SeasonViewModel, SeasonEditViewModel>, ISeasonsListViewModel
    {
        #region Fields

        private readonly IAdminService _adminService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SeasonsListViewModel"/>.
        /// </summary>
        public SeasonsListViewModel(IAdminService adminService)
        {
            _adminService = adminService;

            Title = AdministrationResources.SeasonsTitle;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetEditViewType()
        {
            return typeof(SeasonEditView);
        }

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(SeasonViewModel item)
        {
            _adminService.RemoveSeason(item.ToDto<SeasonDto>());
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _adminService.UpdateSeasonsOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _adminService.GetSeasonsList();

            Items = new ObservableCollection<SeasonViewModel>(result.OrderBy(x => x.Order).ToViewModels<SeasonViewModel>());
        }

        #endregion Methods
    }
}