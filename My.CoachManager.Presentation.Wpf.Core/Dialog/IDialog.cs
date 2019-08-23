namespace My.CoachManager.Presentation.Wpf.Core.Dialog
{
    /// <summary>
    /// Interface representing a framework dialog.
    /// </summary>
    public interface IDialog
    {
        /// <summary>
        /// Opens a framework dialog with specified owner.
        /// </summary>
        /// <returns>
        /// true if user clicks the OK button; otherwise false.
        /// </returns>
        bool? ShowDialog();
    }
}
