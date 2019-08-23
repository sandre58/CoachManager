using System.Linq;
using System.Reflection;
using GalaSoft.MvvmLight.Command;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.Navigation;

namespace My.CoachManager.Presentation.Wpf.Core.Commands
{
    public static class NavigationCommandsManager
    {
        static NavigationCommandsManager()
        {
            NavigationManager.Navigated += (o, e) =>
            {
                GoBackCommand.RaiseCanExecuteChanged();
                GoForwardCommand.RaiseCanExecuteChanged();
            };
        }

        /// <summary>
        /// Gets or sets the go back command.
        /// </summary>
        public static RelayCommand GoBackCommand { get; } = new RelayCommand(GoBack, CanGoBack);

        /// <summary>
        /// Gets or sets the go forward command.
        /// </summary>
        public static RelayCommand GoForwardCommand { get; } = new RelayCommand(GoForward, CanGoForward);

        /// <summary>
        /// Gets Global Navigate command.
        /// </summary>
        public static RelayCommand<string> NavigateCommand => new RelayCommand<string>(s =>
        {
            var assembly = Assembly.GetEntryAssembly();
            var type = (assembly?.GetTypes())?.FirstOrDefault(x => x.Name == s);

            if(type != null) NavigationManager.NavigateTo(type);
        });

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
