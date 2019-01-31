using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Presentation.Core.Dialog;
using My.CoachManager.Presentation.Core.ViewModels;

namespace My.CoachManager.Presentation.Wpf.ViewModels.Dialogs
{
    /// <summary>
    /// ViewModel for the login window.
    /// </summary>
    public class LoginViewModel : DialogViewModel
    {

        #region Properties

        /// <summary>
        /// Gets or sets the username of the user.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string UserName { get; set; }

        /// <summary>
        /// Gets or sets the password of the user.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets error.
        /// </summary>
        public string Error { get; private set; }

        /// <summary>
        /// Gets or sets login action.
        /// </summary>
        public Func<string, string, Tuple<bool, string>> LoginAction { get; set; }

        #endregion Properties

        #region Msthods

        public override void Close(DialogResult? dialogResult)
        {

            if (dialogResult.HasValue && dialogResult.Value == DialogResult.Ok && LoginAction != null)
            {
                var result = LoginAction.Invoke(UserName, Password);

                Error = result.Item2;
                if (result.Item1)
                {
                    base.Close(dialogResult);
                }
            }
            else
            {
                base.Close(dialogResult);
            }
        }

        #endregion
    }
}