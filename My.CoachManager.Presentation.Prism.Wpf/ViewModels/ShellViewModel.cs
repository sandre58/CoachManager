using System;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Global;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Navigation;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.StatusBar.Core;
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
        private IRegionNavigationJournal _journal;
        private INavigatableWorkspaceViewModel _activeWorkspace;

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
            : base(dialogService, eventAggregator, logger)
        {
            _regionManager = regionManager;
            _eventAggregator = eventAggregator;
            WorkspaceDialogInteractionRequest = new InteractionRequest<IDialog>();
            DialogInteractionRequest = new InteractionRequest<IDialog>();
            NotificationPopupInteractionRequest = new InteractionRequest<INotificationPopup>();

            _eventAggregator.GetEvent<ShowWorkspaceDialogRequestEvent>().Subscribe(OnShowWorkspaceDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowCustomDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowMessageDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowNotificationPopupRequestEvent>().Subscribe(OnShowNotificationRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<ShowLoginDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<NavigateRequestEvent>().Subscribe(OnNavigateRequested, ThreadOption.UIThread, true);
            _eventAggregator.GetEvent<NotifyNavigationCompletedEvent>().Subscribe(OnNavigateCompleted, ThreadOption.UIThread, true);

            var navigateCommand = new DelegateCommand<string>(Navigate, s => true);

            GlobalCommands.NavigateCommand.RegisterCommand(navigateCommand);

            GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the active workspace.
        /// </summary>
        public INavigatableWorkspaceViewModel ActiveWorkspace
        {
            get { return _activeWorkspace; }
            private set { SetProperty(ref _activeWorkspace, value); }
        }

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

        /// <summary>
        /// Gets or sets the go back command.
        /// </summary>
        public DelegateCommand GoBackCommand { get; set; }

        /// <summary>
        /// Gets or sets the go forward command.
        /// </summary>
        public DelegateCommand GoForwardCommand { get; set; }

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
            var parameters = e.Parameters != null ? e.Parameters.ToString() : "";
            var newUri = new Uri(e.Path + parameters, UriKind.Relative);
            var currentEntry = _regionManager.Regions[RegionNames.WorkspaceRegion].NavigationService.Journal.CurrentEntry;
            var activeUri = currentEntry != null ? currentEntry.Uri : null;
            if (!Equals(newUri, activeUri))
                _regionManager.RequestNavigate(RegionNames.WorkspaceRegion, newUri);
        }

        /// <summary>
        /// Call when the navigation is completed.
        /// </summary>
        /// <param name="e"></param>
        protected void OnNavigateCompleted(NavigationCompletedEventArgs e)
        {
            _journal = e.Context.NavigationService.Journal;
            ActiveWorkspace = e.Workspace;

            GoForwardCommand.RaiseCanExecuteChanged();
            GoBackCommand.RaiseCanExecuteChanged();

            if (ActiveWorkspace != null)
            {
                _eventAggregator.GetEvent<UpdateStatusBarMessageRequestEvent>().Publish(ActiveWorkspace.Title);
            }
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

        #region GoBack

        /// <summary>
        /// Go back.
        /// </summary>
        public void GoBack()
        {
            if (_journal != null)
            {
                _journal.GoBack();
            }
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        public bool CanGoBack()
        {
            return _journal != null ? _journal.CanGoBack : false;
        }

        #endregion GoBack

        #region GoForward

        /// <summary>
        /// Go back.
        /// </summary>
        public void GoForward()
        {
            if (_journal != null)
            {
                _journal.GoForward();
            }
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        public bool CanGoForward()
        {
            return _journal != null ? _journal.CanGoForward : false;
        }

        #endregion GoForward

        #endregion Methods
    }
}