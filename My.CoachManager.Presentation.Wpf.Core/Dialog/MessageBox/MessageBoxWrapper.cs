using System;
using System.Windows;
using WinMessageBox = System.Windows.MessageBox;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox
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
            return WinMessageBox.Show(
                _settings.MessageBoxText,
                _settings.Caption,
                _settings.Button,
                _settings.Icon,
                _settings.DefaultResult);
        }
    }
}
