using System;

namespace My.CoachManager.Presentation.Mvvm.Core.Dialog
{
    public class DialogEventArgs : EventArgs
    {
        #region Members

        /// <summary>
        /// Gets or set the dialog.
        /// </summary>
        public IWorkspaceDialog Dialog { get; }

        /// <summary>
        /// Gets or sets the callback action.
        /// </summary>
        public Action<IWorkspaceDialog> Callback { get; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="DialogEventArgs"/>.
        /// </summary>
        /// <param name="dialog"></param>
        /// <param name="callback"></param>
        public DialogEventArgs(IWorkspaceDialog dialog, Action<IWorkspaceDialog> callback = null)
        {
            Dialog = dialog;
            Callback = callback;
        }

        #endregion Constructors
    }
}