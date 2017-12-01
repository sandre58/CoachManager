using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.StatusBarModule.ViewModels;
using My.CoachManager.Presentation.Prism.StatusBarModule.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.StatusBarModule
{
    public class StatusBarModuleInit : IModule
    {
        private readonly IRegionManager _regionManager;

        public StatusBarModuleInit(IRegionManager regionManager)
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