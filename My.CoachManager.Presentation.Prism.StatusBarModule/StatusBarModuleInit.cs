using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
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
        private readonly IServiceLocator _serviceLocator;
        private readonly IUnityContainer _container;

        public StatusBarModuleInit(IUnityContainer container, IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _container = container;
        }

        public void Initialize()
        {
            // Register ViewModels
            _container.RegisterType<IStatusBarViewModel, StatusBarViewModel>();

            // Register the view
            _regionManager.RegisterViewWithRegion(RegionNames.StatusBarRegion, () => _serviceLocator.GetInstance<StatusBarView>());
        }
    }
}