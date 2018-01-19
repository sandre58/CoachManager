using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Administration.Views;
using Prism.Modularity;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Modules.Administration
{
    public class AdministrationModule : IModule
    {
        private readonly IRegionManager _regionManager;

        public AdministrationModule(IRegionManager regionManager)
        {
            _regionManager = regionManager;
        }

        public void Initialize()
        {
            // Register ViewModels
            Locator.RegisterType<ICategoriesListViewModel, CategoriesListViewModel>();
            Locator.RegisterType<ICategoryEditViewModel, CategoryEditViewModel>();
            Locator.RegisterType<IPositionsListViewModel, PositionsListViewModel>();
            Locator.RegisterType<IPositionEditViewModel, PositionEditViewModel>();
            Locator.RegisterType<ISeasonsListViewModel, SeasonsListViewModel>();
            Locator.RegisterType<ISeasonEditViewModel, SeasonEditViewModel>();
            Locator.RegisterType<IPlayersListViewModel, PlayersListViewModel>();
            Locator.RegisterType<IPlayerEditViewModel, PlayerEditViewModel>();

            // Register Views (for navigation)
            Locator.RegisterTypeForNavigation<CategoriesListView>();
            Locator.RegisterTypeForNavigation<PositionsListView>();
            Locator.RegisterTypeForNavigation<SeasonsListView>();
            Locator.RegisterTypeForNavigation<PlayersListView>();

            // Register the navigation view
            _regionManager.RegisterViewWithRegion(RegionNames.NavigationRegion, Locator.GetInstance<AdministrationNavigationView>);
        }
    }
}