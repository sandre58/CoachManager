using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;

namespace My.CoachManager.Presentation.Prism.Modules.Login.Core
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