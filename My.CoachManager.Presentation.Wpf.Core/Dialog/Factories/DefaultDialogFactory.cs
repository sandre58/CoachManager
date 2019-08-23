using System;
using System.Windows;
using My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;
using My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile;
using My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.Factories
{
    /// <summary>
    /// Default framework dialog factory that will create instances of standard Windows dialogs.
    /// </summary>
    public class DefaultDialogFactory : IDialogFactory
    {
        /// <inheritdoc />
            public virtual IDialogWindow Create(Type dialogType)
            {
                if (dialogType == null) throw new ArgumentNullException(nameof(dialogType));

                var instance = Activator.CreateInstance(dialogType);

                switch (instance)
                {
                    case IDialogWindow customDialog:
                        return customDialog;

                    case Window dialog:
                        return new DefaultDialogWindowWrapper(dialog);

                    default:
                        throw new ArgumentException($"Only dialogs of type {typeof(Window)} or {typeof(IDialogWindow)} are supported.");
                }
            }
        
        /// <inheritdoc />
        public virtual IMessageBox CreateMessageBox(MessageBoxSettings settings)
        {
            return new MessageBoxWrapper(settings);
        }

        /// <inheritdoc />
        public virtual IDialog CreateOpenFileDialog(OpenFileDialogSettings settings)
        {
            return new OpenFileDialogWrapper(settings);
        }

        /// <inheritdoc />
        public virtual IDialog CreateSaveFileDialog(SaveFileDialogSettings settings)
        {
            return new SaveFileDialogWrapper(settings);
        }

        /// <inheritdoc />
        public virtual IDialog CreateFolderBrowserDialog(FolderBrowserDialogSettings settings)
        {
            return new FolderBrowserDialogWrapper(settings);
        }
    }
}
