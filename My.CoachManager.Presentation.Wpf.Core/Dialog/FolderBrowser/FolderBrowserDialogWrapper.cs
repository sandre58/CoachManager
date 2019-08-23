using System;
using System.Windows.Forms;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser
{
    /// <summary>
    /// Class wrapping <see cref="FolderBrowserDialog"/>.
    /// </summary>
    public sealed class FolderBrowserDialogWrapper : IDialog
    {
        private readonly FolderBrowserDialogSettings _settings;

        /// <summary>
        /// Initializes a new instance of the <see cref="FolderBrowserDialogWrapper"/> class.
        /// </summary>
        /// <param name="settings">The settings for the folder browser dialog.</param>
        public FolderBrowserDialogWrapper(FolderBrowserDialogSettings settings)
        {
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        /// <inheritdoc />
        public bool? ShowDialog()
        {
            using (var dialog = new FolderBrowserDialog())
            {
                var sync = new FolderBrowserDialogSettingsSync(dialog, _settings);

                // Update dialog
                sync.ToDialog();

                DialogResult result = dialog.ShowDialog();

                // Update settings
                sync.ToSettings();

                switch (result)
                {
                    case DialogResult.OK:
                    case DialogResult.Yes:
                        return true;

                    case DialogResult.No:
                    case DialogResult.Abort:
                        return false;

                    default:
                        return null;
                }
            }
        }
    }
}
