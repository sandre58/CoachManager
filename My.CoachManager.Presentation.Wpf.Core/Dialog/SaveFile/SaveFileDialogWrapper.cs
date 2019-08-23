using Microsoft.Win32;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile
{
    /// <summary>
    /// Class wrapping <see cref="SaveFileDialog"/>.
    /// </summary>
    public sealed class SaveFileDialogWrapper : IDialog
    {
        private readonly SaveFileDialog _dialog;
        private readonly SaveFileDialogSettingsSync _sync;

        /// <summary>
        /// Initializes a new instance of the <see cref="SaveFileDialogWrapper"/> class.
        /// </summary>
        /// <param name="settings">The settings for the save file dialog.</param>
        public SaveFileDialogWrapper(SaveFileDialogSettings settings)
        {
            _dialog = new SaveFileDialog();
            _sync = new SaveFileDialogSettingsSync(_dialog, settings);

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
