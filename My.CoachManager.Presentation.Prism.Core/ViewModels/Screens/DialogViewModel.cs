using System;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    public abstract class DialogViewModel : ScreenViewModel, IDialogViewModel
    {
        #region Members

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

        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initialise a new instance of <see cref="DialogViewModel" />.
        /// </summary>
        public DialogViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            DialogResult = DialogResult.None;

            CloseCommand = new DelegateCommand<DialogResult?>(Close, CanClose);
        }

        #endregion Constructors

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
            if (dialogResult != null) DialogResult = dialogResult.Value;
            OnCloseRequest(EventArgs.Empty);
        }

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        /// <param name="e"></param>
        protected virtual void OnCloseRequest(EventArgs e)
        {
            if (CloseRequest != null)
            {
                CloseRequest(this, e);
            }
        }

        #endregion Methods

        #region Events

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public event EventHandler CloseRequest;

        #endregion Events
    }
}