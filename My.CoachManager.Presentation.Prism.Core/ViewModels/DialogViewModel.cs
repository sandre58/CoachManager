using System;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Services;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class DialogViewModel : ScreenViewModel, IDialogViewModel
    {
        #region Members

        /// <summary>
        /// Gets or sets the result.
        /// </summary>
        public DialogResult DialogResult { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ScreenViewModel"/>.
        /// </summary>
        public DialogViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            DialogResult = DialogResult.None;
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Closes the dialog.
        /// </summary>
        public virtual void Close(DialogResult dialogResult = DialogResult.Ok)
        {
            DialogResult = dialogResult;
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