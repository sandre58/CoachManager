using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public interface ILoginViewModel : IDialogViewModel
    {
        /// <summary>
        /// Gets or sets the username.
        /// </summary>
        string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        string Password { get; set; }
    }
}