using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Modules.Home.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.Home.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Modules.Home
{
    public class HomeModule : IModule
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initialise a new instance of <see cref="HomeModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public HomeModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <inheritdoc />
        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterForNavigation<HomeViewModel>();
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.TopNavigationRegion, () => ServiceLocator.Current.GetInstance<HomeNavigationView>());
        }
    }
}