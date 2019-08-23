using System;
using System.Windows.Controls;
using My.CoachManager.Presentation.Wpf.Core.Navigation;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction with toast notification.
    /// </summary>
    public interface INavigationService : GalaSoft.MvvmLight.Views.INavigationService
    {
        /// <summary>
        /// Can go previous page.
        /// </summary>
        bool CanGoBack();

        /// <summary>
        /// Go next page.
        /// </summary>
        void GoForward();

        /// <summary>
        /// Can go next page.
        /// </summary>
        bool CanGoForward();

        /// <summary>
        /// Initiates navigation to the specified target.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="navigationCallback">A callback to execute when the navigation request is completed.</param>
        /// <param name="navigationParameters">The navigation parameters specific to the navigation request.</param>
        void NavigateTo(INavigableWorkspaceViewModel target, Action<NavigationResult> navigationCallback, NavigationParameters navigationParameters);

        /// <summary>
        /// Configure Frame.
        /// </summary>
        /// <param name="frame"></param>
        void ConfigureFrame(Frame frame);

        /// <summary>
        /// Raised when the region is about to be navigated to content.
        /// </summary>
        event EventHandler<NavigationEventArgs> Navigating;

        /// <summary>
        /// Raised when the region is navigated to content.
        /// </summary>
        event EventHandler<NavigationEventArgs> Navigated;

        /// <summary>
        /// Raised when a navigation request fails.
        /// </summary>
        event EventHandler<NavigationFailedEventArgs> NavigationFailed;

        /// <summary>
        /// Gets current workspace.
        /// </summary>
        INavigableWorkspaceViewModel CurrentWorkspace { get; }
    }
}