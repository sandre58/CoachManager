using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Administration.Resources;
using My.CoachManager.Presentation.Modules.Shared.Interfaces;
using My.CoachManager.Presentation.ServiceAgent.SeasonServiceReference;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    public class SeasonsListViewModel : OrderedListViewModel<SeasonModel, ISeasonEditView,ISeasonEditView>
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
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            Title = SeasonResources.SeasonsTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(SeasonModel item) => _seasonService.RemoveSeason(SeasonFactory.Get(item, CrudStatus.Deleted));

        /// <inheritdoc />
        /// <summary>
        /// Validates order in the data source.
        /// </summary>
        protected override void ValidateOrderCore() => _seasonService.UpdateOrders(Items.ToDictionary(c => c.Id, c => c.Order));

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _seasonService.GetSeasons();

            Items = result.Select(SeasonFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #endregion Methods
    }
}