using System;
using System.Collections;
using System.Windows;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Prism.Core.Manager
{
    /// <summary>
    /// Provides methods and properties to display a window dialog.
    /// </summary>
    public static class DialogManager
    {
        #region Fields

        private static IDialogService _dialogService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Dialog Service.
        /// </summary>
        private static IDialogService DialogService => _dialogService ??
                                                              (_dialogService = ServiceLocator.Current.GetInstance<IDialogService>());

        #endregion Members

        #region Workspace Dialog

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="itemId"></param>
        /// <param name="callback">Action executed after result of dialog.</param>
        public static void ShowEditDialog<TEditView>(int itemId, Action<IWorkspaceDialog> callback = null) where TEditView : FrameworkElement
        {
            var view = ServiceLocator.Current.GetInstance<TEditView>();
            var model = view.DataContext as IEditViewModel;
            model?.LoadId(itemId);

            ShowWorkspaceDialog(view, callback);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        public static void ShowWorkspaceDialog(FrameworkElement view, Action<IWorkspaceDialog> callback = null)
        {
            DialogService.ShowWorkspaceDialog(view, callback);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="callback">Action executed after result of dialog.</param>
        public static void ShowWorkspaceDialog<TView>(Action<IWorkspaceDialog> callback = null) where TView : FrameworkElement
        {
            var view = ServiceLocator.Current.GetInstance<TView>();
            ShowWorkspaceDialog(view, callback);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeView">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        public static void ShowWorkspaceDialog(Type typeView, Action<IWorkspaceDialog> callback = null)
        {
            var view = ServiceLocator.Current.GetInstance(typeView) as FrameworkElement;
            ShowWorkspaceDialog(view, callback);
        }

        #endregion

        #region Select Items Dialog

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="selectionMode"></param>
        /// <param name="notSelectableItems"></param>
        public static void ShowSelectItemsDialog(FrameworkElement view, Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null)
        {
            DialogService.ShowSelectItemsDialog(view, callback, selectionMode, notSelectableItems);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="selectionMode"></param>
        /// <param name="notSelectableItems"></param>
        public static void ShowSelectItemsDialog<TView>(Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null) where TView : FrameworkElement
        {
            var view = ServiceLocator.Current.GetInstance<TView>();
            ShowSelectItemsDialog(view, callback, selectionMode, notSelectableItems);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeView">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="selectionMode"></param>
        /// <param name="notSelectableItems"></param>
        public static void ShowSelectItemsDialog(Type typeView, Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null)
        {
            var view = ServiceLocator.Current.GetInstance(typeView) as FrameworkElement;
            ShowSelectItemsDialog(view, callback, selectionMode, notSelectableItems);
        }

        #endregion

        #region Message Dialog

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static DialogResult ShowInformationDialog(string message,
            MessageDialogButtons buttons = MessageDialogButtons.Okcancel)
        {
            return ShowMessageDialog(DialogResources.Information, message, MessageDialogType.Information, buttons);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static DialogResult ShowErrorDialog(string message,
            MessageDialogButtons buttons = MessageDialogButtons.Okcancel)
        {
            return ShowMessageDialog(DialogResources.Error, message, MessageDialogType.Error, buttons);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static DialogResult ShowWarningDialog(string message,
            MessageDialogButtons buttons = MessageDialogButtons.Okcancel)
        {
            return ShowMessageDialog(DialogResources.Warning, message, MessageDialogType.Warning, buttons);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        public static DialogResult ShowSuccessDialog(string message)
        {
            return ShowMessageDialog(DialogResources.Success, message, MessageDialogType.Success, MessageDialogButtons.Ok);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        public static DialogResult ShowQuestionDialog(string message)
        {
            return ShowMessageDialog(DialogResources.Question, message, MessageDialogType.Question, MessageDialogButtons.YesNo);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        public static DialogResult ShowQuestionCancelDialog(string message)
        {
            return ShowMessageDialog(DialogResources.Question, message, MessageDialogType.Question, MessageDialogButtons.YesNoCancel);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="title">Title of window.</param>
        /// <param name="message">Message.</param>
        /// <param name="style">Style of window.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static DialogResult ShowMessageDialog(string title, string message,
            MessageDialogType style = MessageDialogType.Information,
            MessageDialogButtons buttons = MessageDialogButtons.Okcancel)
        {
            return DialogService.ShowMessageDialog(title, message, style, buttons);
        }

        #endregion

        #region Custom Dialog

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="title">Title of window.</param>
        public static DialogResult ShowCustomDialog(FrameworkElement view, string title)
        {
            return DialogService.ShowCustomDialog(view, title);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="title">Title of window.</param>
        public static DialogResult ShowCustomDialog<TView>(string title) where TView : FrameworkElement
        {
            var view = ServiceLocator.Current.GetInstance<TView>();
            return ShowCustomDialog(view, title);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeView">The view to include in workspace dialog.</param>
        /// <param name="title">Title of window.</param>
        public static DialogResult ShowCustomDialog(Type typeView, string title)
        {
            var view = ServiceLocator.Current.GetInstance(typeView) as FrameworkElement;
            return ShowCustomDialog(view, title);
        }

        #endregion

        #region OpenFile Dialog

        /// <summary>
        /// Show the dialog for open a file.
        /// </summary>
        public static string ShowOpenFileDialog(string filter = "", bool multiselect = false, string initialDirectory = "",
            bool restoreDirectory = false)
        {
            return DialogService.ShowOpenFileDialog(filter, multiselect, initialDirectory);
        }

        /// <summary>
        /// Show the dialog for open a file.
        /// </summary>
        public static string ShowOpenImagesDialog(bool multiselect = false, string initialDirectory = "",
            bool restoreDirectory = false)
        {
            return ShowOpenFileDialog(DialogResources.AllImages, multiselect, initialDirectory, restoreDirectory);
        }

        #endregion

        #region Login Dialog

        /// <summary>
        /// Show the dialog to provide Username and password.
        /// </summary>
        /// <param name="loginAction">Action to log in.</param>
        /// <param name="login">The login.</param>
        /// <param name="password">The password.</param>
        /// <returns>Item 1 : IsConnected ; Item2 : Error</returns>
        public static DialogResult ShowLoginDialog(Func<string, string, Tuple<bool, string>> loginAction, string login = "", string password = "")
        {
            return DialogService.ShowLoginDialog(loginAction, login, password);
        }

#endregion
    }
}