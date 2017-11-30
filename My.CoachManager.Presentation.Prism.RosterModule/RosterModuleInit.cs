using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.RosterModule.ViewModels;
using My.CoachManager.Presentation.Prism.RosterModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.RosterModule
{
    public class RosterModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initialise a new instance of <see cref="RosterModuleInit"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public RosterModuleInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<IPlayersListViewModel, PlayersListViewModel>();

            // Register Views (for navigation)
            Locator.RegisterTypeForNavigation<PlayersListView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, Locator.GetInstance<RosterNavigationView>);
        }
    }
}