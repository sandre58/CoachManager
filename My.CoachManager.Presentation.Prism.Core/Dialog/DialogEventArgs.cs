using System;

namespace My.CoachManager.Presentation.Prism.Core.Dialog
{
    public class DialogEventArgs : EventArgs
    {
        #region Members

        /// <summary>
        /// Gets or set the dialog.
        /// </summary>
        public IDialog Dialog { get; set; }

        /// <summary>
        /// Gets or sets the callback action.
        /// </summary>
        public Action<IDialog> Callback { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="DialogEventArgs"/>.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="callback"></param>
        public DialogEventArgs(IDialog dialog, Action<IDialog> callback = null)
        {
            Dialog = dialog;
            Callback = callback;
        }

        #endregion Constructors
    }
}