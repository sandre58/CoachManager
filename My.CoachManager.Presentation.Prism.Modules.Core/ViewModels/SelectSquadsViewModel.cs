using System.Linq;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Core.ViewModels
{
    public class SelectSquadsViewModel : SelectItemsViewModel<SquadModel>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets roster Id.
        /// </summary>
        public int RosterId { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets if we can refresh after initialisation.
        /// </summary>
        public override bool RefreshOnInit => false;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SelectSquadsViewModel"/>.
        /// </summary>
        public SelectSquadsViewModel(IRosterService rosterService)
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
        protected override ObservableItemsCollection<SquadModel> LoadItems()
        {
            if (RosterId > 0)
            {
                var result = _rosterService.GetSquads(RosterId);

                return result.Select(SquadFactory.Get).ToItemsObservableCollection();
            }
            return  new ObservableItemsCollection<SquadModel>();
        }

        #endregion Data

        #region PropertyChanged

        protected virtual void OnRosterIdChanged()
        {
            Refresh();
        }

        #endregion
        #endregion Methods
    }
}