using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Global;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Modules.About.Core;
using My.CoachManager.Presentation.Prism.Modules.About.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.About.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.About.Views;
using Prism.Commands;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.About
{
    public class AboutModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IDialogService _dialogService;

        /// <summary>
        /// Initialise a new instance of <see cref="AboutModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="dialogService"></param>
        public AboutModule(IRegionManager regionManager, IDialogService dialogService)
        {
            _regionManager = regionManager;
            _dialogService = dialogService;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<IAboutViewModel, AboutViewModel>();
            Locator.RegisterType<IAboutView, AboutView>();

            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, Locator.GetInstance<AboutCommand>);

            var showAboutCommand = new DelegateCommand(ShowAbout);

            GlobalCommands.ShowAboutViewCommand.RegisterCommand(showAboutCommand);
        }

        /// <summary>
        /// Show About View.
        /// </summary>
        public void ShowAbout()
        {
            _dialogService.ShowCustomDialog<IAboutView>(AboutResources.About);
        }
    }
}