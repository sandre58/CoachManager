using System;
using My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;
using My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile;
using My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog
{
    /// <summary>
    /// Interface responsible for creating framework dialogs.
    /// </summary>
    public interface IDialogFactory
    {
        /// <summary>
        /// Creates a <see cref="IDialogWindow"/> of specified type.
        /// </summary>
        IDialogWindow Create(Type dialogType);

        /// <summary>
        /// Create an instance of the Windows message box.
        /// </summary>
        /// <param name="settings">The settings for the message box.</param>
        IMessageBox CreateMessageBox(MessageBoxSettings settings);

        /// <summary>
        /// Create an instance of the Windows open file dialog.
        /// </summary>
        /// <param name="settings">The settings for the open file dialog.</param>
        IDialog CreateOpenFileDialog(OpenFileDialogSettings settings);

        /// <summary>
        /// Create an instance of the Windows save file dialog.
        /// </summary>
        /// <param name="settings">The settings for the save file dialog.</param>
        IDialog CreateSaveFileDialog(SaveFileDialogSettings settings);

        /// <summary>
        /// Create an instance of the Windows folder browser dialog.
        /// </summary>
        /// <param name="settings">The settings for the folder browser dialog.</param>
        IDialog CreateFolderBrowserDialog(FolderBrowserDialogSettings settings);
    }
}
