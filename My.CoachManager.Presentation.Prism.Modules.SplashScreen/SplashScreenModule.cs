using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.SplashScreen.ViewModels;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.SplashScreen
{
    public class SplashScreenModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public SplashScreenModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<ISplashScreenViewModel, SplashScreenViewModel>();
        }
    }
}