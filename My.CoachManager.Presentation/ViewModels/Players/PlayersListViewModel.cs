using System.Collections.ObjectModel;
using My.CoachManager.Application.Dtos.Players;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.PlayerServiceReference;
using My.CoachManager.Presentation.ViewModels.Core;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Players
{
    /// <summary>
    /// ViewModel for the settings window.
    /// </summary>
    public class PlayersListViewModel : ListViewModel<PlayerViewModel, EditPlayerViewModel, PlayerFiltersViewModel>
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        /// <param name="filters">The filters.</param>
        public PlayersListViewModel(PlayerFiltersViewModel filters)
            : base(filters)
        {
        }

        /// <summary>
        /// Constructor without paramaters used by the designer.
        /// </summary>
        public PlayersListViewModel()
            : this(new PlayerFiltersViewModel())
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
                return Resources.Strings.Screens.PlayersResources.PlayersList;
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Can Filter ?
        /// </summary>
        /// <returns></returns>
        public override bool CanFilter()
        {
            return !string.IsNullOrEmpty(Filters.LastName) || !string.IsNullOrEmpty(Filters.FirstName);
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            using (var client = ServiceClientFactory.Create<PlayerServiceClient, IPlayerService>())
            {
                var result = client.GetPlayersInformations(new PlayerFiltersDto()
                {
                    LastName = ActiveFilters.LastName,
                    FirstName = ActiveFilters.FirstName
                });

                //Entities = new ObservableCollection<PlayerViewModel>(result.Results.ToViewModels<PlayerViewModel>());
                //Count = result.Total;
            }
        }

        protected override void RemoveItemCore(PlayerViewModel item)
        {
            throw new System.NotImplementedException();
        }

        protected override void OpenItemCore(PlayerViewModel item)
        {
            throw new System.NotImplementedException();
        }

        #endregion Methods
    }
}