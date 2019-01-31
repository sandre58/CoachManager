using System.Linq;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Modules.Shared.ViewModels
{
    public class SelectRostersViewModel : SelectItemsViewModel<RosterModel>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SelectPlayersViewModel"/>.
        /// </summary>
        public SelectRostersViewModel(IRosterService rosterService)
        {
            _rosterService = rosterService;
        }

        #endregion Constructors

        #region Methods

        #region Data

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override ObservableItemsCollection<RosterModel> LoadItems()
        {
            var result = _rosterService.GetRosters();

            return result.Select(RosterFactory.Get).ToItemsObservableCollection();
        }

        #endregion Data

        #endregion Methods
    }
}