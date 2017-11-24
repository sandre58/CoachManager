using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.RosterModule.ViewModels;
using My.CoachManager.Presentation.Prism.RosterModule.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.RosterModule
{
    public class RosterModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;
        private readonly IUnityContainer _container;

        /// <summary>
        /// Initialise a new instance of <see cref="RosterModule"/>.
        /// </summary>
        /// <param name="container"></param>
        /// <param name="regionManager"></param>
        /// <param name="serviceLocator"></param>
        public RosterModule(IUnityContainer container, IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _container = container;
        }

        /// <summary>
        /// Initializes the module.
        /// </summary>
        public void Initialize()
        {
            // Register ViewModels
            _container.RegisterType<IRosterViewModel, RosterViewModel>();

            // Register Views (for navigation)
            _container.RegisterTypeForNavigation<RosterView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => _serviceLocator.GetInstance<RosterNavigationView>());
        }
    }
}