using System;

namespace My.CoachManager.Presentation.Wpf.Core.Navigation
{
    /// <summary>
    /// EventArgs used with the Navigated event.
    /// </summary>
    public class NavigationFailedEventArgs : NavigationEventArgs
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Wpf.Core.Navigation.NavigationFailedEventArgs" /> class.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        public NavigationFailedEventArgs(NavigationContext navigationContext) : base(navigationContext)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Wpf.Core.Navigation.NavigationFailedEventArgs" /> class.
        /// </summary>
        /// <param name="navigationContext">The navigation context.</param>
        /// <param name="error">The error.</param>
        public NavigationFailedEventArgs(NavigationContext navigationContext, Exception error)
            : base(navigationContext)
        {
            Error = error;
        }

        /// <summary>
        /// Gets the error.
        /// </summary>
        /// <value>The <see cref="Exception"/>, or <see langword="null"/> if the failure was not caused by an exception.</value>
        public Exception Error { get; }
    }
}
