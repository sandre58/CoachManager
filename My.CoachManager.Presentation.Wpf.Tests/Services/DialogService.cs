using System;
using Microsoft.Win32;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Tests.Services
{
    /// <inheritdoc />
    /// <summary>
    /// Class abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public class DialogService : IDialogService
    {
        #region IDialogService Members

        /// <inheritdoc />
        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        public void ShowWorkspaceDialog(IWorkspaceDialogViewModel view, Action<IWorkspaceDialog> callback = null)
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="title">Title of window.</param>
        /// <param name="message">Message.</param>
        /// <param name="style">Style of window.</param>
        /// <param name="buttons">Buttons of window.</param>
        public DialogResult ShowMessageDialog(string title, string message, MessageDialogType style = MessageDialogType.Information, MessageDialogButtons buttons = MessageDialogButtons.Okcancel)
        {
            return DialogResult.Ok;
        }

        /// <inheritdoc />
        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="title">Title of window.</param>
        public DialogResult ShowCustomDialog(IDialogViewModel view, string title)
        {
            return DialogResult.Ok;
        }

        /// <inheritdoc />
        /// <summary>
        /// Show the dialog for open a file.
        /// </summary>
        public string ShowOpenFileDialog(string filter = "", bool multiselect = false, string initialDirectory = "", bool restoreDirectory = false)
        {
            var dialog = new OpenFileDialog
            {
                Filter = filter,
                Multiselect = multiselect,
                InitialDirectory = initialDirectory,
                RestoreDirectory = restoreDirectory
            };

            var result = dialog.ShowDialog();

            if (result.HasValue && result.Value)
                return dialog.FileName;

            return string.Empty;
        }

        /// <summary>
        /// Show the dialog to provide Username and password.
        /// </summary>
        /// <param name="loginAction">Action to log in.</param>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns>Item 1 : IsConnected ; Item2 : Error</returns>
        public DialogResult ShowLoginDialog(Func<string, string, Tuple<bool, string>> loginAction, string login = "", string password = "")
        {
            return DialogResult.Ok;
        }

        #endregion IDialogService Members
    }
}