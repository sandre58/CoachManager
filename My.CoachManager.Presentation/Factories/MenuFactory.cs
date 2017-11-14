using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Core.Factories.Interfaces;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;
using My.CoachManager.Presentation.Resources.Strings.Screens;
using My.CoachManager.Presentation.ViewModels.Admin;
using My.CoachManager.Presentation.ViewModels.Home;
using My.CoachManager.Presentation.ViewModels.Players;
using My.CoachManager.Presentation.ViewModels.Staff;

namespace My.CoachManager.Presentation.Factories
{
    public class MenuFactory : IMenuFactory<INavigatable>
    {
        /// <summary>
        /// Create the screen view model.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<MenuItemViewModel<INavigatable>> Create()
        {
            var menu = new ObservableCollection<MenuItemViewModel<INavigatable>>();

            if (System.Windows.Application.Current.MainWindow == null) return menu;

            // Main
            var homeMenu = new MenuItemViewModel<INavigatable>(MenuResources.Home, ScreenLocator.Create<HomeViewModel>());
            var squadMenu = new MenuItemViewModel<INavigatable>(MenuResources.Squad, null);
            var adminMenu = new MenuItemViewModel<INavigatable>(MenuResources.Admin, null);

            menu.Add(homeMenu);
            menu.Add(squadMenu);
            menu.Add(adminMenu);

            // Squad
            var playersMenu = new MenuItemViewModel<INavigatable>(MenuResources.Players, ScreenLocator.Create<PlayersListViewModel>(), squadMenu);
            var staffMenu = new MenuItemViewModel<INavigatable>(MenuResources.Staff, ScreenLocator.Create<StaffListViewModel>(), squadMenu);
            squadMenu.SubItems.Add(playersMenu);
            squadMenu.SubItems.Add(staffMenu);

            // Admin
            var categoriesMenu = new MenuItemViewModel<INavigatable>(MenuResources.Categories, ScreenLocator.Create<CategoriesListViewModel>(), adminMenu);
            var seasonsMenu = new MenuItemViewModel<INavigatable>(MenuResources.Seasons, ScreenLocator.Create<CategoriesListViewModel>(), adminMenu);
            var positionsMenu = new MenuItemViewModel<INavigatable>(MenuResources.Positions, ScreenLocator.Create<CategoriesListViewModel>(), adminMenu);
            adminMenu.SubItems.Add(categoriesMenu);
            adminMenu.SubItems.Add(seasonsMenu);
            adminMenu.SubItems.Add(positionsMenu);

            // Icons
            var current = System.Windows.Application.Current.MainWindow;
            if (current != null)
            {
                squadMenu.Icon = current.FindResource("GroupIconStyle");
                homeMenu.Icon = current.FindResource("Home1IconStyle");
                adminMenu.Icon = current.FindResource("CogIconStyle");
            }

            return menu;
        }
    }
}