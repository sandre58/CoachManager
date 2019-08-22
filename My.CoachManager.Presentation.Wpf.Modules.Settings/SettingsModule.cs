using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Modules.Settings.Views;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Modules.Settings
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
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            // throw new System.NotImplementedException();
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void OnInitialized(IContainerProvider containerProvider)
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