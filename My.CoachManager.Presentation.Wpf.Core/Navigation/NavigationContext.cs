using System;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.Navigation
{
 /// <summary>
    /// Encapsulates information about a navigation request.
    /// </summary>
    public class NavigationContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="NavigationContext"/> class for a region name and a 
        /// <see cref="Uri"/>.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="navigationParameters">The navigation parameters.</param>
        public NavigationContext(INavigableWorkspaceViewModel workspace, NavigationParameters navigationParameters = null)
        {
            Workspace = workspace;
            Parameters = navigationParameters;
        }

        /// <summary>
        /// Gets the navigation URI.
        /// </summary>
        /// <value>The navigation URI.</value>
        public INavigableWorkspaceViewModel Workspace { get; }

        /// <summary>
        /// Gets the <see cref="NavigationParameters"/> extracted from the URI and the object parameters passed in navigation.
        /// </summary>
        /// <value>The URI query.</value>
        public NavigationParameters Parameters { get; }

    }
}
