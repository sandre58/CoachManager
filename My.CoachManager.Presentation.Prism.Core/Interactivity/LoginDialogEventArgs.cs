using System;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity
{
    public class LoginDialogEventArgs : DialogEventArgs
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="LoginDialogEventArgs"/>.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="callback"></param>
        public LoginDialogEventArgs(ILoginDialog dialog, Action<IDialog> callback = null) : base(dialog, callback)
        {
        }

        #endregion Constructors
    }
}