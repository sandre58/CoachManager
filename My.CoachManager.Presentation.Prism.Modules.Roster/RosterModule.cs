using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Roster
{
    public class RosterModule : IModule
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initialise a new instance of <see cref="RosterModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public RosterModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<IPlayerFiltersViewModel, PlayerFiltersViewModel>();
            Locator.RegisterType<IPlayersListViewModel, PlayersListViewModel>();
            Locator.RegisterType<IPlayerViewModel, PlayerViewModel>();

            // Register Views (for navigation)
            Locator.RegisterTypeForNavigation<PlayersListView>();
            Locator.RegisterTypeForNavigation<PlayerView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, Locator.GetInstance<RosterNavigationView>);
        }
    }
}