using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.StatusBar.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.StatusBar.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.StatusBar
{
    public class StatusBarModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public StatusBarModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<IStatusBarViewModel, StatusBarViewModel>();

            // Register the view
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, Locator.GetInstance<StatusBarView>);
        }
    }
}