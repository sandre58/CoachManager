using System;
using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public class PositionsListViewModel : OrderedListViewModel<PositionViewModel>, IPositionsListViewModel
    {
        #region Fields

        private readonly IPositionService _positionService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PositionsListViewModel"/>.
        /// </summary>
        public PositionsListViewModel(IPositionService positionService)
        {
            _positionService = positionService;
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
            return typeof(PositionEditView);
        }

        #endregion Abstract Methods

        #region Initialization

        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = AdministrationResources.PositionsTitle;
        }

        #endregion Initialization

        #region Data

        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(PositionViewModel item)
        {
            _positionService.Remove(item.ToDto<PositionDto>());
        }

        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore()
        {
            _positionService.UpdateOrders(Items.ToDictionary(c => c.Id, c => c.Order));
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _positionService.GetList();

            Items = new ObservableCollection<PositionViewModel>(result.OrderBy(x => x.Order).ToViewModels<PositionViewModel>());
        }

        #endregion Data

        #endregion Methods
    }
}