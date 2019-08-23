using System;
using System.Windows;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Dialog.Factories;
using My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;
using My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile;
using My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile;
using My.CoachManager.Presentation.Wpf.Core.Services;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Services
{
    /// <summary>
    ///     Class abstracting the interaction between view models and views when it comes to
    ///     opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public class DialogService : IDialogService
    {
        private readonly IDialogFactory _dialogFactory;

        /// <summary>
        ///     Initializes a new instance of the <see cref="DialogService" /> class.
        /// </summary>
        public DialogService(IDialogFactory frameworkDialogFactory = null)
        {
            _dialogFactory = frameworkDialogFactory ?? new DefaultDialogFactory();
        }

        #region IDialogService Members

        /// <inheritdoc />
        public void Show(Type dialogType, IDialogViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (dialogType == null) throw new ArgumentNullException(nameof(dialogType));

            var dialog = CreateDialog(dialogType, viewModel);
            dialog.Show();
        }

        /// <inheritdoc />
        public bool? ShowDialog(Type dialogType, IDialogViewModel viewModel)
        {
            if (viewModel == null) throw new ArgumentNullException(nameof(viewModel));
            if (dialogType == null) throw new ArgumentNullException(nameof(dialogType));

            var dialog = CreateDialog(dialogType, viewModel);
            return dialog.ShowDialog();
        }

        /// <inheritdoc />
        public MessageBoxResult ShowMessageBox(
            string messageBoxText,
            string caption = "",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.None)
        {
            var settings = new MessageBoxSettings
            {
                MessageBoxText = messageBoxText,
                Caption = caption,
                Button = button,
                Icon = icon,
                DefaultResult = defaultResult
            };

            return ShowMessageBox(settings);
        }

        /// <inheritdoc />
        public MessageBoxResult ShowMessageBox(
            MessageBoxSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            var messageBox = _dialogFactory.CreateMessageBox(settings);
            return messageBox.Show();
        }

        /// <inheritdoc />
        public bool? ShowOpenFileDialog(
            OpenFileDialogSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            return _dialogFactory
                .CreateOpenFileDialog(settings)
                .ShowDialog();
        }

        /// <inheritdoc />
        public bool? ShowSaveFileDialog(
            SaveFileDialogSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            return _dialogFactory
                .CreateSaveFileDialog(settings)
                .ShowDialog();
        }

        /// <inheritdoc />
        public bool? ShowFolderBrowserDialog(
            FolderBrowserDialogSettings settings)
        {
            if (settings == null) throw new ArgumentNullException(nameof(settings));

            return _dialogFactory
                .CreateFolderBrowserDialog(settings)
                .ShowDialog();
        }

        #endregion

        #region Create Dialogs

        /// <summary>
        /// Create a dialog window.
        /// </summary>
        /// <param name="dialogType"></param>
        /// <param name="viewModel"></param>
        /// <returns></returns>
        private IDialogWindow CreateDialog(Type dialogType, IDialogViewModel viewModel)
        {
            var dialog = _dialogFactory.Create(dialogType);
            dialog.DataContext = viewModel;
            dialog.Title = viewModel.Title;

            return dialog;
        }

        #endregion
    }
}