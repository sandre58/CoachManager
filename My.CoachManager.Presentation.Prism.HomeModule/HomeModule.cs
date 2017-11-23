using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Prism.Home.Views;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Home.ViewModels;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.Home
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;
        private readonly IUnityContainer _container;

        public HomeModule(IUnityContainer container, IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _container = container;
        }

        public void Initialize()
        {
            // Register ViewModels
            _container.RegisterType<IHomeViewModel, HomeViewModel>();

            // Register Views (for navigation)
            _container.RegisterTypeForNavigation<HomeView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => _serviceLocator.GetInstance<HomeNavigationView>());
        }
    }
}