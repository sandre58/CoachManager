using System;
using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.Services.Interfaces
{
    /// <summary>
    /// The navigation service interface.
    /// </summary>
    public interface IMenuNavigationService : INavigationService<INavigatable>
    {
        /// <summary>
        /// Gets or sets the item menu selected.
        /// </summary>
        MenuItemViewModel SelectedItemMenu { get; }

        /// <summary>
        /// Gets or sets the item menu selected.
        /// </summary>
        MenuItemViewModel SelectedFirstLevelItemMenu { get; }

        /// <summary>
        /// Get the menu.
        /// </summary>
        ObservableCollection<MenuItemViewModel<INavigatable>> Menu { get; }

        /// <summary>
        /// Navigue to the menu.
        /// </summary>
        /// <param name="menuItem"></param>
        void NavigateToMenu(MenuItemViewModel<INavigatable> menuItem);
    }
}