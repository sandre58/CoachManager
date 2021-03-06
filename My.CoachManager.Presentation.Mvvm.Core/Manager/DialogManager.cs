﻿using System;
using System.Collections;
using System.Collections.Generic;
using My.CoachManager.Presentation.Mvvm.Core.Constants;
using My.CoachManager.Presentation.Mvvm.Core.Dialog;
using My.CoachManager.Presentation.Mvvm.Core.Enums;
using My.CoachManager.Presentation.Mvvm.Core.Services;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels;
using My.CoachManager.Presentation.Mvvm.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Mvvm.Core.Manager
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
        /// <param name="id"></param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="parameters"></param>
        public static void ShowEditDialog<TEditView>(int id, Action<IWorkspaceDialog> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null) where TEditView : IEditViewModel
        {
            var view = ServiceLocator.Current.GetInstance<TEditView>();
            var p = new List<KeyValuePair<string, object>>(parameters ?? new List<KeyValuePair<string, object>>())
            {
                new KeyValuePair<string, object>(ParametersConstants.Id, id)
            };

            ShowWorkspaceDialog((IWorkspaceDialogViewModel)view, callback, p);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="parameters"></param>
        public static void ShowWorkspaceDialog<TView>(Action<IWorkspaceDialog> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null) where TView : IWorkspaceDialogViewModel
        {
            var view = ServiceLocator.Current.GetInstance<TView>();
            ShowWorkspaceDialog(view, callback, parameters);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeView">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="parameters"></param>
        public static void ShowWorkspaceDialog(Type typeView, Action<IWorkspaceDialog> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var view = ServiceLocator.Current.GetInstance(typeView) as IWorkspaceDialogViewModel;
            ShowWorkspaceDialog(view, callback, parameters);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="parameters"></param>
        public static void ShowWorkspaceDialog(IWorkspaceDialogViewModel view, Action<IWorkspaceDialog> callback = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            SetParameters(view, parameters);
            DialogService.ShowWorkspaceDialog(view, callback);
        }

        #endregion Workspace Dialog

        #region Select Items Dialog

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="selectionMode"></param>
        /// <param name="notSelectableItems"></param>
        /// <param name="parameters"></param>
        public static void ShowSelectItemsDialog<TView>(Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null, IEnumerable<KeyValuePair<string, object>> parameters = null) where TView : ISelectItemsViewModel
        {
            var view = ServiceLocator.Current.GetInstance<TView>();
            ShowSelectItemsDialog(view, callback, selectionMode, notSelectableItems, parameters);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeView">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="selectionMode"></param>
        /// <param name="notSelectableItems"></param>
        /// <param name="parameters"></param>
        public static void ShowSelectItemsDialog(Type typeView, Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var view = ServiceLocator.Current.GetInstance(typeView) as ISelectItemsViewModel;
            ShowSelectItemsDialog(view, callback, selectionMode, notSelectableItems, parameters);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="callback">Action executed after result of dialog.</param>
        /// <param name="selectionMode"></param>
        /// <param name="notSelectableItems"></param>
        /// <param name="parameters"></param>
        public static void ShowSelectItemsDialog(ISelectItemsViewModel view, Action<IWorkspaceDialog> callback = null, SelectionMode selectionMode = SelectionMode.Single, IList notSelectableItems = null, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            view.SelectionMode = selectionMode;

            if (notSelectableItems != null)
                view.NotSelectableItems = notSelectableItems;

            ShowWorkspaceDialog(view, callback, parameters);
        }

        #endregion Select Items Dialog

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

        #endregion Message Dialog

        #region Custom Dialog

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="view">The view to include in workspace dialog.</param>
        /// <param name="title">Title of window.</param>
        public static DialogResult ShowCustomDialog(IDialogViewModel view, string title)
        {
            return DialogService.ShowCustomDialog(view, title);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="title">Title of window.</param>
        public static DialogResult ShowCustomDialog<TView>(string title) where TView : IDialogViewModel
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
            var view = ServiceLocator.Current.GetInstance(typeView) as IDialogViewModel;
            return ShowCustomDialog(view, title);
        }

        #endregion Custom Dialog

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

        #endregion OpenFile Dialog

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

        #endregion Login Dialog

        #region Methods

        /// <summary>
        /// Sets parameters.
        /// </summary>
        /// <param name="view"></param>
        /// <param name="parameters"></param>
        /// <param name="refresh"></param>
        private static void SetParameters(IDialogViewModel view, IEnumerable<KeyValuePair<string, object>> parameters = null, bool refresh = true)
        {
            if (parameters != null && view != null)
            {
                foreach (var x in parameters)
                {
                    view.SetParameter(x.Key, x.Value);
                }
            }

            if (refresh && view is IRefreshable refreshable && refreshable.State == ScreenState.NotLoaded)
            {
                refreshable.Refresh();
            }
        }

        #endregion Methods
    }
}