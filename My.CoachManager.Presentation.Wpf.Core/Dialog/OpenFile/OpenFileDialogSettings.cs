namespace My.CoachManager.Presentation.Wpf.Core.Dialog.OpenFile
{
    /// <summary>
    /// Settings for OpenFileDialog.
    /// </summary>
    public class OpenFileDialogSettings : FileDialogSettings
    {
        /// <summary>
        /// Gets or sets a value indicating whether the dialog box allows multiple files to be
        /// selected.
        /// </summary>
        public bool Multiselect { get; set; }
    }
}
