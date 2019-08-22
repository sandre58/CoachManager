using CommonServiceLocator;
using My.CoachManager.Presentation.Wpf.Core;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Modules.About.Resources;
using My.CoachManager.Presentation.Wpf.Modules.About.ViewModels;
using My.CoachManager.Presentation.Wpf.Modules.About.Views;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Events;
using Prism.Ioc;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Wpf.Modules.About
{
    public class AboutModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        /// <summary>
        /// Initialise a new instance of <see cref="AboutModule"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        public AboutModule(IRegionManager regionManager, IEventAggregator eventAggregator)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
        }

        /// <summary>
        /// Used to register types with the container that will be used by your application.
        /// </summary>
        public void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.Register<AboutViewModel>();
        }

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void OnInitialized(IContainerProvider containerProvider)
        {
            // Register toolbar
            _regionManager.RegisterViewWithRegion(RegionNames.ToolbarRegion, ServiceLocator.Current.GetInstance<AboutCommand>);

            _eventAggregator.GetEvent<ShowAboutViewRequestEvent>().Subscribe(() => DialogManager.ShowCustomDialog<AboutViewModel>(AboutResources.About));
        }
    }
}