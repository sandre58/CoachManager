using System.Collections.ObjectModel;
using My.CoachManager.Presentation.Core.ViewModels.Screens;

namespace My.CoachManager.Presentation.Core.Factories.Interfaces
{
    /// <summary>
    /// Create a menu.
    /// </summary>
    public interface IMenuFactory
    {
        /// <summary>
        /// Create the screen view model.
        /// </summary>
        /// <returns></returns>
        ObservableCollection<MenuItemViewModel> Create();
    }

    /// <summary>
    /// Create a menu.
    /// </summary>
    public interface IMenuFactory<T>
    {
        /// <summary>
        /// Create the screen view model.
        /// </summary>
        /// <returns></returns>
        ObservableCollection<MenuItemViewModel<T>> Create();
    }
}