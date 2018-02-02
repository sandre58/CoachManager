using System.Linq;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class PlayerViewModel : ItemViewModel<PlayerDetailViewModel>, IPlayerViewModel
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerViewModel"/>.
        /// </summary>
        public PlayerViewModel(IRosterService rosterService) : this()
        {
            _rosterService = rosterService;
        }

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerViewModel"/>.
        /// </summary>
        public PlayerViewModel()
        {
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerDetailViewModel LoadItemCore(int id)
        {
            var item = _rosterService.GetPlayers(1).FirstOrDefault(x => x.Id == id);
            return item.ToViewModel<PlayerDetailViewModel>();
        }

        #endregion Methods
    }
}