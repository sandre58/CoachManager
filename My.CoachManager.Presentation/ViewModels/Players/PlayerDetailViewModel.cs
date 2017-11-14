using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.PlayerServiceReference;
using My.CoachManager.Presentation.ViewModels.Core;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Players
{
    internal class PlayerDetailViewModel : DetailViewModel<PlayersListViewModel, PlayerViewModel>
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        /// <param name="id">The item id.</param>
        public PlayerDetailViewModel(int id)
            : base(id)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Item != null ? Item.FullName : Resources.Strings.Screens.PlayersResources.UnknownPlayer;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            using (var client = ServiceClientFactory.Create<PlayerServiceClient, IPlayerService>())
            {
                Item = client.GetById(ItemId).ToViewModel<PlayerViewModel>();
            }
        }

        #endregion Methods
    }
}