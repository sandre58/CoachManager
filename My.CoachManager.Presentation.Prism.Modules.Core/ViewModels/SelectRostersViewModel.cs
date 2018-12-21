using System.Linq;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Core.ViewModels
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