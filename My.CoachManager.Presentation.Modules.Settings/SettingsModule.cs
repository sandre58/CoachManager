using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Presentation.Core;
using My.CoachManager.Presentation.Modules.Settings.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Modules.Settings
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

        /// <inheritdoc />
        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register toolbar
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, ServiceLocator.Current.GetInstance<SettingsCommand>);

            // Register views
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsRegion, ServiceLocator.Current.GetInstance<SkinSettingsView>);

            // Register flyouts
            _regionManager.RegisterViewWithRegion(RegionNames.FlyoutsRegion, ServiceLocator.Current.GetInstance<SettingsView>);

        }
    }
}