using System;
using System.Collections;
using System.Windows;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.Enums;

namespace My.CoachManager.Presentation.Core.Services
{
    /// <summary>
    /// Interface abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public interface IDialogService
    {
        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        void ShowWorkspaceDialog(FrameworkElement view, Action<IWorkspaceDialog> callback = null);

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="title">Title of window.</param>
        /// <param name="message">Message.</param>
        /// <param name="style">Style of window.</param>
        /// <param name="buttons">Buttons of window.</param>
        DialogResult ShowMessageDialog(string title, string message,
            MessageDialogType style = MessageDialogType.Information,
            MessageDialogButtons buttons = MessageDialogButtons.Okcancel);

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="title">Title of window.</param>
        DialogResult ShowCustomDialog(FrameworkElement view, string title);

        /// <summary>
        /// Show the dialog for open a file.
        /// </summary>
        string ShowOpenFileDialog(string filter = "", bool multiselect = false, string initialDirectory = "",
            bool restoreDirectory = false);

        /// <summary>
        /// Show the dialog to provide Username and password.
        /// </summary>
        /// <param name="loginAction">Action to log in.</param>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns>Item 1 : IsConnected ; Item2 : Error</returns>
        DialogResult ShowLoginDialog(Func<string, string, Tuple<bool, string>> loginAction, string login = "", string password = "");
    }
}