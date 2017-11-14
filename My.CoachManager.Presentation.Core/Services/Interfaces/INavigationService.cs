using System;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.Services.Interfaces
{
    /// <summary>
    /// The navigation service interface.
    /// </summary>
    public interface INavigationService<TWorkspaceViewModel> where TWorkspaceViewModel : INavigatable
    {
        /// <summary>
        /// Gets or sets the current selected view.
        /// </summary>
        TWorkspaceViewModel CurrentView { get; }

        /// <summary>
        /// Navigates to the view specified by the key.
        /// </summary>
        void NavigateToView<T>() where T : TWorkspaceViewModel;

        /// <summary>
        /// Navigates to the view specified by the key.
        /// </summary>
        void NavigateToView(Type typeViewModel);

        /// <summary>
        /// Navigates to the view specified by the key.
        /// </summary>
        void NavigateToView(TWorkspaceViewModel viewModel);

        /// <summary>
        /// Go back.
        /// </summary>
        void GoBack();

        /// <summary>
        /// Can Back ?
        /// </summary>
        /// <returns></returns>
        bool CanGoBack();

        /// <summary>
        /// Notify the view change.
        /// </summary>
        /// <returns></returns>
        event EventHandler ViewChanged;
    }
}