using System;
using Microsoft.Win32;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.SaveFile
{
    /// <summary>
    /// Class responsible for synchronizing settings between <see cref="SaveFileDialogSettings" />
    /// and <see cref="SaveFileDialog" />.
    /// </summary>
    internal class SaveFileDialogSettingsSync
    {
        private readonly SaveFileDialog _dialog;
        private readonly SaveFileDialogSettings _settings;

        public SaveFileDialogSettingsSync(SaveFileDialog dialog, SaveFileDialogSettings settings)
        {
            _dialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void ToDialog()
        {
            _dialog.AddExtension = _settings.AddExtension;
            _dialog.CheckFileExists = _settings.CheckFileExists;
            _dialog.CheckPathExists = _settings.CheckPathExists;
            _dialog.CreatePrompt = _settings.CreatePrompt;
            _dialog.DefaultExt = _settings.DefaultExt;
            _dialog.FileName = _settings.FileName;
            _dialog.Filter = _settings.Filter;
            _dialog.FilterIndex = _settings.FilterIndex;
            _dialog.InitialDirectory = _settings.InitialDirectory;
            _dialog.OverwritePrompt = _settings.OverwritePrompt;
            _dialog.Title = _settings.Title;
        }

        public void ToSettings()
        {
            _settings.FileName = _dialog.FileName;
            _settings.FileNames = _dialog.FileNames;
            _settings.FilterIndex = _dialog.FilterIndex;
        }
    }
}
