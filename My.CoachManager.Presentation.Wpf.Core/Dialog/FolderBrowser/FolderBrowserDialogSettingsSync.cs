using System;
using System.Windows.Forms;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.FolderBrowser
{
    /// <summary>
    /// Class responsible for synchronizing settings between
    /// <see cref="FolderBrowserDialogSettings" /> and <see cref="FolderBrowserDialog" />.
    /// </summary>
    internal class FolderBrowserDialogSettingsSync
    {
        private readonly FolderBrowserDialog _dialog;
        private readonly FolderBrowserDialogSettings _settings;

        public FolderBrowserDialogSettingsSync(FolderBrowserDialog dialog, FolderBrowserDialogSettings settings)
        {
            _dialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void ToDialog()
        {
            _dialog.Description = _settings.Description;
            _dialog.SelectedPath = _settings.SelectedPath;
            _dialog.ShowNewFolderButton = _settings.ShowNewFolderButton;
        }

        public void ToSettings()
        {
            _settings.SelectedPath = _dialog.SelectedPath;
        }
    }
}
