using Microsoft.Win32;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile
{
    /// <summary>
    /// Class wrapping <see cref="OpenFileDialog"/>.
    /// </summary>
    public sealed class OpenFileDialogWrapper : IDialog
    {
        private readonly OpenFileDialog _dialog;
        private readonly OpenFileDialogSettingsSync _sync;

        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileDialogWrapper"/> class.
        /// </summary>
        /// <param name="settings">The settings for the open file dialog.</param>
        public OpenFileDialogWrapper(OpenFileDialogSettings settings)
        {
            _dialog = new OpenFileDialog();
            _sync = new OpenFileDialogSettingsSync(_dialog, settings);

            // Update dialog
            _sync.ToDialog();
        }

        /// <inheritdoc />
        public bool? ShowDialog()
        {
            bool? result = _dialog.ShowDialog();

            // Update settings
            _sync.ToSettings();

            return result;
        }
    }
}
