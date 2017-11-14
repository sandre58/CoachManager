using System;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;

namespace My.CoachManager.Presentation.Prism.Core.Interactivity
{
    public class MessageDialogEventArgs : DialogEventArgs
    {
        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="MessageDialogEventArgs"/>.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="callback"></param>
        public MessageDialogEventArgs(IMessageDialog dialog, Action<IDialog> callback = null) : base(dialog, callback)
        {
        }

        #endregion Constructors
    }
}