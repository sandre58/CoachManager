using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Modules.Administration.Resources;
using My.CoachManager.Presentation.Modules.Administration.Views;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using System.Linq;

namespace My.CoachManager.Presentation.Modules.Administration.ViewModels
{
    /// <inheritdoc />
    public class RostersListViewModel : ListViewModel<RosterModel, RosterEditView, RosterEditView>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoriesListViewModel"/>.
        /// </summary>
        public RostersListViewModel(IRosterService rosterService)
        {
            _rosterService = rosterService;
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

            Title = RosterResources.RostersTitle;
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(RosterModel item) => _rosterService.RemoveRoster(RosterFactory.Get(item, CrudStatus.Deleted));

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _rosterService.GetRosters();

            Items = result.Select(RosterFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #endregion Methods
    }
}