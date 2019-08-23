using System;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.Navigation
{
    /// <summary>
    /// EventArgs used with the Navigated event.
    /// </summary>
    public class NavigationEventArgs : EventArgs
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationEventArgs"/> class.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public NavigationEventArgs(NavigationContext navigationContext)
        {
            NavigationContext = navigationContext ?? throw new ArgumentNullException(nameof(navigationContext));
        }

        /// <summary>
        /// Gets the navigation context.
        /// </summary>
        /// <value>The navigation context.</value>
        public NavigationContext NavigationContext { get; }

        /// <summary>
        /// Gets the navigation URI
        /// </summary>
        /// <value>The URI.</value>
        /// <remarks>
        /// This is a convenience accessor around NavigationContext.Uri.
        /// </remarks>
        public INavigableWorkspaceViewModel Workspace => NavigationContext?.Workspace;
    }
}
