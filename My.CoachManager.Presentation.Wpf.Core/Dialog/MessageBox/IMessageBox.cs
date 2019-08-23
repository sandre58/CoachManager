using System.Windows;

namespace My.CoachManager.Presentation.Wpf.Core.Dialog.MessageBox
{
    /// <summary>
    /// Interface representing a message box.
    /// </summary>
    public interface IMessageBox
    {
        /// <summary>
        /// Opens a message box with specified owner.
        /// </summary>
        /// <returns>
        /// A <see cref="MessageBoxResult"/> value that specifies which message box button is
        /// clicked by the user.
        /// </returns>
        MessageBoxResult Show();
    }
}
