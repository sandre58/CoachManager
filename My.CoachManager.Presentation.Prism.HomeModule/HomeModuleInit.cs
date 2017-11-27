using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.HomeModule.ViewModels;
using My.CoachManager.Presentation.Prism.HomeModule.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.HomeModule
{
    public class HomeModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;
        private readonly IUnityContainer _container;

        public HomeModuleInit(IUnityContainer container, IRegionManager regionManager, IServiceLocator serviceLocator)
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