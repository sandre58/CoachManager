using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Administration
{
    public class AdministrationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public AdministrationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, typeof(AdministrationNavigationView));
        }
    }
}