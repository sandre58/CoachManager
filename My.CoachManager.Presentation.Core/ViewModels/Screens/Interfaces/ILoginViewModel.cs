namespace My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces
{
    /// <summary>
    /// A view model representing a workspace.
    /// </summary>
    public interface ILoginViewModel : IDialogViewModel
    {
        /// <summary>
        /// Get or Set the user name.
        /// </summary>
        string Username { get; set; }

        /// <summary>
        /// Get or Set the password.
        /// </summary>
        string Password { get; set; }
    }
}