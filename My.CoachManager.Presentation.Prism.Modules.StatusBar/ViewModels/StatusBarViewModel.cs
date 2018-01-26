﻿using System.Reflection;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.StatusBar.Core;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.StatusBar.ViewModels
{
    internal class StatusBarViewModel : ScreenViewModel, IStatusBarViewModel
    {
        #region Fields

        private string _copyright;
        private string _version;
        private string _message;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="StatusBarViewModel"/>.
        /// </summary>
        /// <param name="eventAggregator"></param>
        /// <param name="dialogService"></param>
        /// <param name="logger"></param>
        public StatusBarViewModel(IEventAggregator eventAggregator, IDialogService dialogService, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            eventAggregator.GetEvent<UpdateStatusBarMessageRequestEvent>().Subscribe(OnMessageChanged, ThreadOption.UIThread, true);

            var assembly = Assembly.GetEntryAssembly();

            var copyrightAttr = assembly.GetCustomAttribute<AssemblyCopyrightAttribute>();

            Version = string.Format(GlobalResources.Version, assembly.GetName().Version);
            Copyright = (copyrightAttr != null) ? copyrightAttr.Copyright : "";

            Message = StatusResources.DefaultStatusMessage;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets the copyright.
        /// </summary>
        public string Copyright
        {
            get { return _copyright; }
            private set { SetProperty(ref _copyright, value); }
        }

        /// <summary>
        /// Gets the version.
        /// </summary>
        public string Version
        {
            get { return _version; }
            private set { SetProperty(ref _version, value); }
        }

        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        public string Message
        {
            get { return _message; }
            set { SetProperty(ref _message, value); }
        }

        #endregion Members

        #region Methods

        /// <summary>
        /// Call when the message changed.
        /// </summary>
        /// <param name="message"></param>
        protected void OnMessageChanged(string message)
        {
            Message = message;
        }

        #endregion Methods
    }
}