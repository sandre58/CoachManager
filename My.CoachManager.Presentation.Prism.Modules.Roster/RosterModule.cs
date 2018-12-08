using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Modules.Roster
{
    public class RosterModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public RosterModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => ServiceLocator.Current.GetInstance<RosterNavigationView>());

            // Register the workspace view
            _container.RegisterTypeForNavigation<RosterView>();
            //_container.RegisterTypeForNavigation<SeasonsListView>();
            //_container.RegisterTypeForNavigation<CategoriesListView>();
            //_container.RegisterTypeForNavigation<PlayersListView>();
        }
    }
}