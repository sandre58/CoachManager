using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Forms;
using CommonServiceLocator;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.Presentation.Wpf.Core.Constants;
using My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;
using My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile;
using My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile;
using My.CoachManager.Presentation.Wpf.Core.Ioc;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.Manager
{
    /// <summary>
    /// Provides methods and properties to display a window dialog.
    /// </summary>
    public static class DialogManager
    {
        #region Fields

        private static IDialogService _dialogService;
        private static IViewModelTypeLocator _viewModelTypeLocator;
        private static IViewModelProvider _viewModelProvider;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets Dialog Service.
        /// </summary>
        private static IDialogService DialogService => _dialogService ??
                                                       (_dialogService = ServiceLocator.Current.GetInstance<IDialogService>());

        /// <summary>
        /// Gets Dialog Service.
        /// </summary>
        private static IViewModelTypeLocator ViewModelTypeLocator => _viewModelTypeLocator ??
                                                       (_viewModelTypeLocator = ServiceLocator.Current.GetInstance<IViewModelTypeLocator>());

        /// <summary>
        /// Gets Dialog Service.
        /// </summary>
        private static IViewModelProvider ViewModelProvider => _viewModelProvider ??
                                                       (_viewModelProvider = ServiceLocator.Current.GetInstance<IViewModelProvider>());

        #endregion Members

        #region Show

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="viewModel">The view to include in workspace dialog.</param>
        public static void Show(IDialogViewModel viewModel)
        {
            var viewType = ViewModelTypeLocator.LocateView(viewModel.GetType());
            DialogService.Show(viewType, viewModel);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        public static void Show<TViewModel>() where TViewModel : IDialogViewModel
        {
            var viewType = ViewModelTypeLocator.LocateView(typeof(TViewModel));
            var viewModel = ViewModelProvider.GetViewModel<TViewModel>();
            DialogService.Show(viewType, viewModel);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeViewModel">The view to include in workspace dialog.</param>
        public static void Show(Type typeViewModel)
        {
            var viewType = ViewModelTypeLocator.LocateView(typeViewModel);
            var viewModel = (IDialogViewModel)ViewModelProvider.GetViewModel(typeViewModel);
            DialogService.Show(viewType, viewModel);
        }

        #endregion

        #region ShowDialog

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        public static bool? ShowDialog<TViewModel>(IEnumerable<KeyValuePair<string, object>> parameters = null) where TViewModel : IDialogViewModel
        {
            return ShowDialog(typeof(TViewModel), parameters);
        }

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="typeViewModel">The view to include in workspace dialog.</param>
        /// <param name="parameters"></param>
        public static bool? ShowDialog(Type typeViewModel, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var viewModel = (IDialogViewModel)ViewModelProvider.GetViewModel(typeViewModel);
            return ShowDialog(viewModel, parameters);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="viewModel">The view to include in workspace dialog.</param>
        /// <param name="parameters"></param>
        public static bool? ShowDialog(IDialogViewModel viewModel, IEnumerable<KeyValuePair<string, object>> parameters = null)
        {
            var viewType = ViewModelTypeLocator.LocateView(viewModel.GetType());
            SetParameters(viewModel, parameters);
            return DialogService.ShowDialog(viewType, viewModel);
        }

        #endregion

        #region ShowWorkspaceDialog

        /// <summary>
        /// Displays a modal dialog.
        /// </summary>
        /// <param name="id"></param>
        /// <param name="parameters"></param>
        public static bool? ShowEditDialog<TEditViewModel>(int id, IEnumerable<KeyValuePair<string, object>> parameters = null) where TEditViewModel : IEditViewModel
        {
            var p = new List<KeyValuePair<string, object>>(parameters ?? new List<KeyValuePair<string, object>>())
            {
                new KeyValuePair<string, object>(ParametersConstants.Id, id)
            };

            return ShowDialog<TEditViewModel>(p);
        }

        
        #endregion
        
        #region MessageBox

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static MessageBoxResult ShowInformationDialog(string message,
            MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
            return ShowMessageBox(message, DialogResources.Information, buttons, MessageBoxImage.Information, MessageBoxResult.OK);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static MessageBoxResult ShowErrorDialog(string message,
            MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
            return ShowMessageBox(message, DialogResources.Error, buttons, MessageBoxImage.Error, MessageBoxResult.OK);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="buttons">Buttons of window.</param>
        public static MessageBoxResult ShowWarningDialog(string message,
            MessageBoxButton buttons = MessageBoxButton.OKCancel)
        {
            return ShowMessageBox(message, DialogResources.Warning, buttons, MessageBoxImage.Warning, MessageBoxResult.OK);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        public static MessageBoxResult ShowQuestionDialog(string message)
        {
            return ShowMessageBox(message, DialogResources.Question, MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.Yes);
        }

        /// <summary>
        /// Displays a message dialog.
        /// </summary>
        /// <param name="message">Message.</param>
        public static MessageBoxResult ShowQuestionCancelDialog(string message)
        {
            return ShowMessageBox(message, DialogResources.Question, MessageBoxButton.YesNoCancel, MessageBoxImage.Question, MessageBoxResult.Yes);
        }


        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and
        /// that accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="messageBoxText">
        /// A <see cref="string"/> that specifies the text to display.
        /// </param>
        /// <param name="caption">
        /// A <see cref="string"/> that specifies the title bar caption to display. Default value
        /// is an empty string.
        /// </param>
        /// <param name="button">
        /// A <see cref="MessageBoxButton"/> value that specifies which button or buttons to
        /// display. Default value is <see cref="MessageBoxButton.OK"/>.
        /// </param>
        /// <param name="icon">
        /// A <see cref="MessageBoxImage"/> value that specifies the icon to display. Default value
        /// is <see cref="MessageBoxImage.None"/>.
        /// </param>
        /// <param name="defaultResult">
        /// A <see cref="MessageBoxResult"/> value that specifies the default result of the
        /// message box. Default value is <see cref="MessageBoxResult.None"/>.
        /// </param>
        /// <returns>
        /// A <see cref="MessageBoxResult"/> value that specifies which message box button is
        /// clicked by the user.
        /// </returns>
        public static MessageBoxResult ShowMessageBox(
            string messageBoxText,
            string caption = "",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            return DialogService.ShowMessageBox(messageBoxText, caption, button, icon, defaultResult);
        }

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and
        /// that accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="settings">The settings for the message box dialog.</param>
        /// <returns>
        /// A <see cref="MessageBoxResult"/> value that specifies which message box button is
        /// clicked by the user.
        /// </returns>
        public static MessageBoxResult ShowMessageBox(
            MessageBoxSettings settings)
        {
            return DialogService.ShowMessageBox(settings);
        }

        #endregion

        #region Files

        /// <summary>
        /// Displays the <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="settings">The settings for the open file dialog.</param>
        /// <returns>
        /// If the user clicks the OK button of the dialog that is displayed, true is returned;
        /// otherwise false.
        /// </returns>
        public static bool? ShowOpenFileDialog(
            OpenFileDialogSettings settings)
        {
            return DialogService.ShowOpenFileDialog(settings);
        }

        /// <summary>
        /// Displays the <see cref="SaveFileDialog"/>.
        /// </summary>
        /// <param name="settings">The settings for the save file dialog.</param>
        /// <returns>
        /// If the user clicks the OK button of the dialog that is displayed, true is returned;
        /// otherwise false.
        /// </returns>
        public static bool? ShowSaveFileDialog(
            SaveFileDialogSettings settings)
        {
            return DialogService.ShowSaveFileDialog(settings);
        }

        /// <summary>
        /// Displays the <see cref="FolderBrowserDialog"/>.
        /// </summary>
        /// <param name="settings">The settings for the folder browser dialog.</param>
        /// <returns>
        /// If the user clicks the OK button of the dialog that is displayed, true is returned;
        /// otherwise false.
        /// </returns>
        public static bool? ShowFolderBrowserDialog(
            FolderBrowserDialogSettings settings)
        {
            return DialogService.ShowFolderBrowserDialog(settings);
        }

        #endregion

        #region Methods

        /// <summary>
        /// Sets parameters.
        /// </summary>
        /// <param name="viewModel"></param>
        /// <param name="parameters"></param>
        /// <param name="refresh"></param>
        private static void SetParameters(IDialogViewModel viewModel, IEnumerable<KeyValuePair<string, object>> parameters = null, bool refresh = true)
        {
            if (parameters != null && viewModel != null)
            {
                foreach (var x in parameters)
                {
                    viewModel.SetParameter(x.Key, x.Value);
                }
            }

            if (refresh && viewModel is IRefreshable refreshable)
            {
                refreshable.Refresh();
            }
        }

        #endregion Methods
    }
}
