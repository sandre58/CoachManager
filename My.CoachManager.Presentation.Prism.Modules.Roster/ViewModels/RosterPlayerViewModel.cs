using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public partial class RosterPlayerViewModel : ItemViewModel<RosterPlayerModel>
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public RosterPlayerViewModel(IRosterService rosterService)
        {
            _rosterService = rosterService;
        }

        #endregion Constructors

        #region Data

        protected override RosterPlayerModel LoadItemCore(int id)
        {
            return RosterFactory.Get(_rosterService.GetRosterPlayerById(id));
        }

        #endregion Data

        #region PropertyChanged

        /// <summary>
        /// Calls when Item changes.
        /// </summary>
        protected override void OnItemChanged()
        {
            base.OnItemChanged();

            if (Item.Player != null)
                Title = Item.Player.FullName;
        }

        #endregion PropertyChanged
    }
}