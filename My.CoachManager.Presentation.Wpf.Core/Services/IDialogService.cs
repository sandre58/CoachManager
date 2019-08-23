using System;
using System.Windows;
using System.Windows.Forms;
using My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;
using My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile;
using My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Wpf.Core.Services
{
   /// <summary>
    /// Interface abstracting the interaction between view models and views when it comes to
    /// opening dialogs using the MVVM pattern in WPF.
    /// </summary>
    public interface IDialogService
   {
        /// <summary>
        /// Displays a non-modal custom dialog of specified type.
        /// </summary>
        /// <param name="viewModel">The view model of the new custom dialog.</param>
        /// <param name="dialogType">The type of the custom dialog to show.</param>
        void Show(Type dialogType, IDialogViewModel viewModel);

        /// <summary>
        /// Displays a modal custom dialog of specified type.
        /// </summary>
        /// <param name="viewModel">The view model of the new custom dialog.</param>
        /// <param name="dialogType">The type of the custom dialog to show.</param>
        bool? ShowDialog(Type dialogType, IDialogViewModel viewModel);

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
        MessageBoxResult ShowMessageBox(
            string messageBoxText,
            string caption = "",
            MessageBoxButton button = MessageBoxButton.OK,
            MessageBoxImage icon = MessageBoxImage.None,
            MessageBoxResult defaultResult = MessageBoxResult.None);

        /// <summary>
        /// Displays a message box that has a message, title bar caption, button, and icon; and
        /// that accepts a default message box result and returns a result.
        /// </summary>
        /// <param name="settings">The settings for the message box dialog.</param>
        /// <returns>
        /// A <see cref="MessageBoxResult"/> value that specifies which message box button is
        /// clicked by the user.
        /// </returns>
        MessageBoxResult ShowMessageBox(
            MessageBoxSettings settings);

        /// <summary>
        /// Displays the <see cref="OpenFileDialog"/>.
        /// </summary>
        /// <param name="settings">The settings for the open file dialog.</param>
        /// <returns>
        /// If the user clicks the OK button of the dialog that is displayed, true is returned;
        /// otherwise false.
        /// </returns>
        bool? ShowOpenFileDialog(
            OpenFileDialogSettings settings);

        /// <summary>
        /// Displays the <see cref="SaveFileDialog"/>.
        /// </summary>
        /// <param name="settings">The settings for the save file dialog.</param>
        /// <returns>
        /// If the user clicks the OK button of the dialog that is displayed, true is returned;
        /// otherwise false.
        /// </returns>
        bool? ShowSaveFileDialog(
            SaveFileDialogSettings settings);

        /// <summary>
        /// Displays the <see cref="FolderBrowserDialog"/>.
        /// </summary>
        /// <param name="settings">The settings for the folder browser dialog.</param>
        /// <returns>
        /// If the user clicks the OK button of the dialog that is displayed, true is returned;
        /// otherwise false.
        /// </returns>
        bool? ShowFolderBrowserDialog(
            FolderBrowserDialogSettings settings);
    }
}
