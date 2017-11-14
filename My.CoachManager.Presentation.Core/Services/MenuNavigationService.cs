using System.Collections.ObjectModel;
using System.Linq;
using My.CoachManager.Presentation.Core.Factories.Interfaces;
using My.CoachManager.Presentation.Core.Services.Interfaces;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.Services
{
    /// <summary>
    /// The implementation of the contract <see cref="IMenuNavigationService"/>.
    /// this class has no need on its ownself, hence explicit implementation.
    /// </summary>
    public class MenuNavigationService : NavigationService<INavigatable>, IMenuNavigationService
    {
        #region Fields

        private MenuItemViewModel<INavigatable> _selectedMenu;
        private readonly ObservableCollection<MenuItemViewModel<INavigatable>> _menu;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuNavigationService()
        {
            _menu = new ObservableCollection<MenuItemViewModel<INavigatable>>();
        }

        /// <summary>
        /// Default constructor.
        /// </summary>
        public MenuNavigationService(IMenuFactory<INavigatable> f)
            : this()
        {
            _menu = f.Create();
        }

        #endregion Constructor

        #region Properties

        /// <summary>
        /// Gets or sets the item menu selected.
        /// </summary>
        public MenuItemViewModel SelectedItemMenu
        {
            get { return _selectedMenu; }
        }

        /// <summary>
        /// Gets or sets the item menu selected.
        /// </summary>
        public MenuItemViewModel SelectedFirstLevelItemMenu
        {
            get { return GetFirstLevelOfMenu(SelectedItemMenu); }
        }

        /// <summary>
        /// Get the menu.
        /// </summary>
        public ObservableCollection<MenuItemViewModel<INavigatable>> Menu
        {
            get { return _menu; }
        }

        #endregion Properties

        #region Method

        /// <summary>
        /// Call when view change.
        /// </summary>
        protected override void OnNotifyViewChanged()
        {
            SelectMenuOnNavigated();
            base.OnNotifyViewChanged();
        }

        /// <summary>
        /// Navigate to the menu.
        /// </summary>
        /// <param name="menuItem"></param>
        public void NavigateToMenu(MenuItemViewModel<INavigatable> menuItem)
        {
            if (menuItem == null) return;

            if (menuItem.Target != null)
            {
                NavigateToView(menuItem.Target);
            }
            else
            {
                var subItem = menuItem.SubItems.FirstOrDefault(m => m.Target != null);
                NavigateToMenu(subItem);
            }
        }

        /// <summary>
        /// Update the menu after navigation.
        /// </summary>
        private void SelectMenuOnNavigated()
        {
            _selectedMenu = GetMenuFromView(CurrentView);

            foreach (var menu in Menu)
            {
                UnselectMenu(menu);
            }
            if (SelectedItemMenu != null)
                SelectedItemMenu.IsSelected = true;
        }

        /// <summary>
        /// Un select a menu.
        /// </summary>
        /// <param name="menu"></param>
        private void UnselectMenu(MenuItemViewModel<INavigatable> menu)
        {
            menu.IsSelected = false;

            foreach (var submenu in menu.SubItems)
            {
                UnselectMenu(submenu);
            }
        }

        /// <summary>
        /// Get the menu from view.
        /// </summary>
        /// <param name="type"></param>
        private MenuItemViewModel<INavigatable> GetMenuFromView(INavigatable type)
        {
            foreach (var menu in Menu)
            {
                var result = GetMenuFromView(menu, type);
                if (result != null) return result;
            }

            return null;
        }

        /// <summary>
        /// Get the menu from view.
        /// </summary>
        /// <param name="menu"></param>
        /// <param name="vm"></param>
        private MenuItemViewModel<INavigatable> GetMenuFromView(MenuItemViewModel<INavigatable> menu, INavigatable vm)
        {
            if (menu.Target != null && vm.GetType() == menu.Target.GetType()) return menu;
            foreach (var submenu in menu.SubItems)
            {
                var result = GetMenuFromView(submenu, vm);
                if (result != null) return result;
            }

            return null;
        }

        /// <summary>
        /// Get the first Level of Menu.
        /// </summary>
        /// <param name="menu"></param>
        private MenuItemViewModel GetFirstLevelOfMenu(MenuItemViewModel menu)
        {
            if (menu == null) return null;
            if (menu.Parent != null)
            {
                return GetFirstLevelOfMenu(menu.Parent);
            }

            return menu;
        }

        #endregion Method
    }
}