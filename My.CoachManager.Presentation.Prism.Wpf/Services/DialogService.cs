using System;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Win32;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Events;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using My.CoachManager.Presentation.Prism.Wpf.ViewModels;
using My.CoachManager.Presentation.Prism.Wpf.Views;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Wpf.Services
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
        public void ShowWorkspaceDialog(FrameworkElement view, Action<IWorkspaceDialog> callback = null)
        {

            var dialog = new Dialog()
            {
                Content = view
            };

            ServiceLocator.Current.GetInstance<IEventAggregator>().GetEvent<ShowWorkspaceDialogRequestEvent>().Publish(new DialogEventArgs(dialog, callback));

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
            var vm = new MessageViewModel
            {
                Title = title,
                Message = message,
                Buttons = buttons,
                Type = style
            };
            vm.Initialize();
            
            var result = DialogResult.None; 
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {

                var dialog = new MessageDialog
                {
                    DataContext = vm
                };

                dialog.ShowDialog();

                result = ((IDialogViewModel) dialog.DataContext).DialogResult;
            });
            
            return result;
        }

        /// <inheritdoc />
        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="title">Title of window.</param>
        public DialogResult ShowCustomDialog(FrameworkElement view, string title)
        {
            var result = DialogResult.None;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {

                var dialog = new CustomDialog
                {
                    Content = view,
                    Title = title,
                    DataContext = view.DataContext as IDialogViewModel
                };

                dialog.ShowDialog();

                result = ((IDialogViewModel)dialog.DataContext).DialogResult;
            });

            return result;
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
        public DialogResult ShowLoginDialog(Func<string, string, Tuple<bool, string>> loginAction, string login = "", string password  ="")
        {
            var vm = new LoginViewModel();
            vm.Initialize();

            if(!string.IsNullOrEmpty(login)) vm.UserName = login;
            if (!string.IsNullOrEmpty(password)) vm.Password = password;

            vm.Title = ControlResources.Authentification;
            vm.LoginAction = loginAction;

            var result = DialogResult.None;
            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {

                var dialog = new LoginDialog
                {
                    DataContext = vm
                };

                dialog.ShowDialog();

                result = ((IDialogViewModel)dialog.DataContext).DialogResult;
            });

            return result;
        }

        #endregion IDialogService Members
    }
}