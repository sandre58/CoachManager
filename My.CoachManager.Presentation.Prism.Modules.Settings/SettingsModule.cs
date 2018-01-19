using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Settings.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Settings.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Settings
{
    public class SettingsModule : IModule
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initialise a new instance of <see cref="SettingsModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public SettingsModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<ISettingsViewModel, SettingsViewModel>();
            Locator.RegisterType<ISkinSettingsViewModel, SkinSettingsViewModel>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsRegion, Locator.GetInstance<SkinSettingsView>);

            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, Locator.GetInstance<SettingsCommand>);
            _regionManager.RegisterViewWithRegion(RegionNames.FlyoutsRegion, Locator.GetInstance<SettingsView>);
        }
    }
}