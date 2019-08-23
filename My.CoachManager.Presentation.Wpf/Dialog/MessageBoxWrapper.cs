using System;
using System.Windows;
using My.CoachManager.Presentation.Wpf.Controls;
using My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox;

namespace My.CoachManager.Presentation.Wpf.Dialog
{
    /// <summary>
    /// Class wrapping WinMessageBox.
    /// </summary>
    public sealed class MessageBoxWrapper : IMessageBox
    {
        private readonly MessageBoxSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxWrapper"/> class.
        /// </summary>
        /// <param name="settings">The settings for the folder browser dialog.</param>
        public MessageBoxWrapper(MessageBoxSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />
        public MessageBoxResult Show()
        {
            var dialog = new MessageDialog
            {
                Title = _settings.Caption,
                Content = _settings.MessageBoxText,
                Image = _settings.Icon,
                Button = _settings.Button,
                DefaultResult = _settings.DefaultResult
            };

            return dialog.ShowDialog();
        }
    }
}
