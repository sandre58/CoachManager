using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Core;
using My.CoachManager.Presentation.Modules.Training.Views;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Modules.Training
{
    public class TrainingModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        public TrainingModule(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
        }

        public void Initialize()
        {
            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => ServiceLocator.Current.GetInstance<TrainingNavigationView>());

            // Register the workspace view
            _container.RegisterTypeForNavigation<TrainingsView>();
            _container.RegisterTypeForNavigation<TrainingView>();
        }
    }
}