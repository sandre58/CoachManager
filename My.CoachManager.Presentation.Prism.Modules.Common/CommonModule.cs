using System.Windows.Input;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Commands;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Modules.Common.Resources;
using My.CoachManager.Presentation.Prism.Modules.Common.Views;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Common
{
    public class CommonModule : IModule
    {
        private readonly IRegionManager _regionManager;

        /// <summary>
        /// Initialise a new instance of <see cref="CommonModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public CommonModule(IRegionManager regionManager)
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
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, () => ServiceLocator.Current.GetInstance<AboutCommand>());
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, ServiceLocator.Current.GetInstance<SettingsCommand>);

            // Register views
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, () => ServiceLocator.Current.GetInstance<StatusBarView>());
            _regionManager.RegisterViewWithRegion(RegionNames.SettingsRegion, ServiceLocator.Current.GetInstance<SkinSettingsView>);

            // Register flyouts
            _regionManager.RegisterViewWithRegion(RegionNames.FlyoutsRegion, ServiceLocator.Current.GetInstance<SettingsView>);

            var showAboutCommand = new DelegateCommand(() =>
            {
                DialogManager.ShowCustomDialog(typeof(AboutView), AboutResources.About);
            });

            GlobalCommands.ShowAboutViewCommand.RegisterCommand(showAboutCommand);
            KeyboardManager.RegisterGlobalShortcut(new KeyBinding(showAboutCommand, Key.F1, ModifierKeys.None));
        }
    }
}