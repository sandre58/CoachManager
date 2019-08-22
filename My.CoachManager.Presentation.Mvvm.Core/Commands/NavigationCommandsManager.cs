using My.CoachManager.Presentation.Mvvm.Core.Manager;

namespace My.CoachManager.Presentation.Mvvm.Core.Commands
{
    public static class NavigationCommandsManager
    {
        /// <summary>
        /// Gets or sets the go back command.
        /// </summary>
        public static DelegateCommand GoBackCommand { get; } = new DelegateCommand(GoBack, CanGoBack);

        /// <summary>
        /// Gets or sets the go forward command.
        /// </summary>
        public static DelegateCommand GoForwardCommand { get; } = new DelegateCommand(GoForward, CanGoForward);

        #region GoBack

        /// <summary>
        /// Go back.
        /// </summary>
        private static void GoBack()
        {
            NavigationManager.GoBack();
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private static bool CanGoBack()
        {
            return NavigationManager.CanGoBack();
        }

        #endregion GoBack

        #region GoForward

        /// <summary>
        /// Go back.
        /// </summary>
        private static void GoForward()
        {
            NavigationManager.GoForward();
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private static bool CanGoForward()
        {
            return NavigationManager.CanGoForward();
        }

        #endregion GoForward
    }
}
