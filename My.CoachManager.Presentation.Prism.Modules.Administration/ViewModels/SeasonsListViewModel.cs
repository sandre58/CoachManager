using System;
using System.Linq;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class SeasonsListViewModel : OrderedListViewModel<SeasonModel>, ISeasonsListViewModel
    {
        #region Fields

        private readonly ISeasonService _seasonService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SeasonsListViewModel"/>.
        /// </summary>
        public SeasonsListViewModel(ISeasonService seasonService)
        {
            _seasonService = seasonService;

            Title = AdministrationResources.SeasonsTitle;
        }

        #endregion Constructors

        #region Methods

        #region Abstract Methods

        /// <summary>
        /// Get Item View Type.
        /// </summary>
        /// <returns></returns>
        protected override Type GetEditViewType()
        {
            return typeof(SeasonEditView);
        }

        #endregion Abstract Methods

        #region Initialization

        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = AdministrationResources.SeasonsTitle;
        }

        #endregion Initialization

        #region Data

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(SeasonModel item)
        {
           // _seasonService.Remove(item.ToDto<SeasonDto>());
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _seasonService.UpdateOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _seasonService.GetList();

            //Items = new ObservableCollection<SeasonViewModel>(result.OrderBy(x => x.Order).ToViewModels<SeasonViewModel>());
        }

        #endregion Data

        #endregion Methods
    }
}