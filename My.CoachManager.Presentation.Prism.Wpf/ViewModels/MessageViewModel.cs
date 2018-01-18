﻿using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    /// <summary>
    /// ViewModel for the login window.
    /// </summary>
    public class MessageViewModel : DialogViewModel, IMessageViewModel
    {
        #region Fields

        private string _message;
        private MessageDialogType _type;
        private MessageDialogStyle _style;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        public MessageDialogType Type
        {
            get { return _type; }
            set { SetProperty(ref _type, value); }
        }

        /// <summary>
        /// Gets or sets the style.
        /// </summary>
        public MessageDialogStyle Style
        {
            get { return _style; }
            set { SetProperty(ref _style, value); }
        }

        /// <summary>
        /// Gets or sets the "OK" Command.
        /// </summary>
        public DelegateCommand OkCommand { get; set; }

        /// <summary>
        /// Gets or sets the "Cancel" Command.
        /// </summary>
        public DelegateCommand CancelCommand { get; set; }

        /// <summary>
        /// Gets or sets the "Yes" Command.
        /// </summary>
        public DelegateCommand YesCommand { get; set; }

        /// <summary>
        /// Gets or sets the "No" Command.
        /// </summary>
        public DelegateCommand NoCommand { get; set; }

        #endregion Properties

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="MessageViewModel"/>.
        /// </summary>
        public MessageViewModel(IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            OkCommand = new DelegateCommand(() => Close(DialogResult.Ok));
            CancelCommand = new DelegateCommand(() => Close(DialogResult.Cancel));
            YesCommand = new DelegateCommand(() => Close(DialogResult.Yes));
            NoCommand = new DelegateCommand(() => Close(DialogResult.No));
        }

        #endregion Constructors
    }
}