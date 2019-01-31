using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Core;
using My.CoachManager.Presentation.Modules.Administration.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Modules.Administration
{
    public class AdministrationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public AdministrationModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => ServiceLocator.Current.GetInstance<AdministrationNavigationView>());

            // Register the workspace view
            _container.RegisterTypeForNavigation<RostersListView>();
            _container.RegisterTypeForNavigation<SeasonsListView>();
            _container.RegisterTypeForNavigation<CategoriesListView>();
            _container.RegisterTypeForNavigation<PlayersListView>();
        }
    }
}