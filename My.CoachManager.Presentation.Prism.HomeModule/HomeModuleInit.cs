using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.HomeModule.ViewModels;
using My.CoachManager.Presentation.Prism.HomeModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.HomeModule
{
    public class HomeModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;

        public HomeModuleInit(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<IHomeViewModel, HomeViewModel>();

            // Register Views (for navigation)
            Locator.RegisterTypeForNavigation<HomeView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, Locator.GetInstance<HomeNavigationView>);
        }
    }
}