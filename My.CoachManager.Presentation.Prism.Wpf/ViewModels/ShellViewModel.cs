using System;
using System.Windows.Input;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.EventAggregator;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Interactivity.InteractionRequest;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public class ShellViewModel : ScreenViewModel, IShellViewModel
    {
        #region Fields

        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator _eventAggregator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ShellViewModel"/>.
        /// </summary>
        /// <param name="regionManager"></param>
        /// <param name="eventAggregator"></param>
        /// <param name="dialogService"></param>
        /// <param name="logger"></param>
        public ShellViewModel(IRegionManager regionManager, IEventAggregator eventAggregator, IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            WorkspaceDialogInteractionRequest = new InteractionRequest<IDialog>();
            DialogInteractionRequest = new InteractionRequest<IDialog>();
            NotificationPopupInteractionRequest = new InteractionRequest<INotificationPopup>();

            _eventAggregator.GetEvent<ShowWorkspaceDialogRequestEvent>().Subscribe(OnShowWorkspaceDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowMessageDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowNotificationPopupRequestEvent>().Subscribe(OnShowNotificationRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowLoginDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<NavigateRequestEvent>().Subscribe(OnNavigateRequested, ThreadOption.UIThread, true);

            NavigateCommand = new DelegateCommand<string>(Navigate);
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or Set the navigate command.
        /// </summary>
        public ICommand NavigateCommand { get; private set; }

        /// <summary>
        /// Gets or Set the dialog interraction.
        /// </summary>
        public InteractionRequest<IDialog> DialogInteractionRequest { get; set; }

        /// <summary>
        /// Gets or Set the dialog interraction.
        /// </summary>
        public InteractionRequest<IDialog> WorkspaceDialogInteractionRequest { get; set; }

        /// <summary>
        /// Gets or Set the dialog interraction.
        /// </summary>
        public InteractionRequest<INotificationPopup> NotificationPopupInteractionRequest { get; set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Navigates between the screen.
        /// </summary>
        /// <param name="navigatePath"></param>
        private void Navigate(string navigatePath)
        {
            if (navigatePath != null)
            {
                _eventAggregator.GetEvent<NavigateRequestEvent>().Publish(new NavigationEventArgs(navigatePath));
            }
        }

        /// <summary>
        /// Call when the navigation is requested.
        /// </summary>
        /// <param name="e"></param>
        protected void OnNavigateRequested(NavigationEventArgs e)
        {
            _regionManager.RequestNavigate(RegionNames.WorkspaceRegion, new Uri(e.Path, UriKind.Relative));
            _eventAggregator.GetEvent<StatusBarMessageEvent>().Publish(e.Path);
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        protected void OnShowWorkspaceDialogRequested(DialogEventArgs e)
        {
            WorkspaceDialogInteractionRequest.Raise(e.Dialog, e.Callback);
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        protected void OnShowDialogRequested(DialogEventArgs e)
        {
            DialogInteractionRequest.Raise(e.Dialog, e.Callback);
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        protected void OnShowNotificationRequested(NotificationEventArgs e)
        {
            NotificationPopupInteractionRequest.Raise(e.Notification, e.Callback);
        }

        #endregion Methods
    }
}