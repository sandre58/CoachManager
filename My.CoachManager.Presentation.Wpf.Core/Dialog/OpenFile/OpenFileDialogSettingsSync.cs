using System;
using Microsoft.Win32;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile
{
    /// <summary>
    /// Class responsible for synchronizing settings between <see cref="OpenFileDialogSettings" />
    /// and <see cref="OpenFileDialog" />.
    /// </summary>
    internal class OpenFileDialogSettingsSync
    {
        private readonly OpenFileDialog _dialog;
        private readonly OpenFileDialogSettings _settings;

        public OpenFileDialogSettingsSync(OpenFileDialog dialog, OpenFileDialogSettings settings)
        {
            _dialog = dialog ?? throw new ArgumentNullException(nameof(dialog));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
        }

        public void ToDialog()
        {
            _dialog.AddExtension = _settings.AddExtension;
            _dialog.CheckFileExists = _settings.CheckFileExists;
            _dialog.CheckPathExists = _settings.CheckPathExists;
            _dialog.DefaultExt = _settings.DefaultExt;
            _dialog.FileName = _settings.FileName;
            _dialog.Filter = _settings.Filter;
            _dialog.FilterIndex = _settings.FilterIndex;
            _dialog.InitialDirectory = _settings.InitialDirectory;
            _dialog.Multiselect = _settings.Multiselect;
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
