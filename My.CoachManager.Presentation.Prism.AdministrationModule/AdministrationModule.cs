using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using My.CoachManager.Presentation.Prism.AdministrationModule.ViewModels;
using My.CoachManager.Presentation.Prism.AdministrationModule.Views;
using My.CoachManager.Presentation.Prism.Core;
using Prism.Modularity;
using Prism.Regions;
using Prism.Unity;

namespace My.CoachManager.Presentation.Prism.AdministrationModule
{
    public class AdministrationModule : IModule
    {
        private readonly IRegionManager _regionManager;
        private readonly IServiceLocator _serviceLocator;
        private readonly IUnityContainer _container;

        public AdministrationModule(IUnityContainer container, IRegionManager regionManager, IServiceLocator serviceLocator)
        {
            _regionManager = regionManager;
            _serviceLocator = serviceLocator;
            _container = container;
        }

        public void Initialize()
        {
            // Register ViewModels
            _container.RegisterType<ICategoriesListViewModel, CategoriesListViewModel>();
            _container.RegisterType<ICategoryEditViewModel, CategoryEditViewModel>();
            _container.RegisterType<IPositionsListViewModel, PositionsListViewModel>();
            _container.RegisterType<IPositionEditViewModel, PositionEditViewModel>();
            _container.RegisterType<ISeasonsListViewModel, SeasonsListViewModel>();
            _container.RegisterType<ISeasonEditViewModel, SeasonEditViewModel>();
            _container.RegisterType<IPlayersListViewModel, PlayersListViewModel>();
            _container.RegisterType<IPlayerEditViewModel, PlayerEditViewModel>();

            // Register Views (for navigation)
            _container.RegisterTypeForNavigation<CategoriesListView>();
            _container.RegisterTypeForNavigation<PositionsListView>();
            _container.RegisterTypeForNavigation<SeasonsListView>();
            _container.RegisterTypeForNavigation<PlayersListView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, () => _serviceLocator.GetInstance<AdministrationNavigationView>());
        }
    }
}