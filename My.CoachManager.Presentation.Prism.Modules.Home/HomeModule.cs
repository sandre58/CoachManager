using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Home.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Home.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Home
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public HomeModule(IRegionManager regionManager)
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