using System;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class DialogViewModel : ScreenViewModel, IDialogViewModel
    {
        #region Members

        public string Title { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public DialogResult DialogResult { get; set; }

        /// <summary>
        /// Gets or sets close command.
        /// </summary>
        public DelegateCommand<DialogResult?> CloseCommand { get; private set; }

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        public override void Initialize()
        {
            base.Initialize();

            KeyboardManager.RegisterWorkspaceDialogShortcuts(KeyboardShortcuts);
            Refresh();
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();
            DialogResult = DialogResult.None;
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            CloseCommand = new DelegateCommand<DialogResult?>(Close, CanClose);
        }

        #endregion Initialisation

        #region Methods

        /// <summary>
        /// Can Close ?
        /// </summary>
        /// <param name="dialogResult"></param>
        /// <returns></returns>
        protected bool CanClose(DialogResult? dialogResult)
        {
            return true;
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public virtual void Close(DialogResult? dialogResult)
        {
            if (dialogResult != null)
            {
                DialogResult = dialogResult.Value;
            }

            KeyboardManager.RemoveWorkspaceDialogShortcuts(KeyboardShortcuts);
            OnCloseRequest(EventArgs.Empty);
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCloseRequest(EventArgs e)
        {
            CloseRequest?.Invoke(this, e);
        }

        #endregion Methods

        #region Events

        /// <inheritdoc />
        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public event EventHandler CloseRequest;

        #endregion Events
    }
}