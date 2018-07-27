using System.Collections.Generic;
using System.Linq;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Global;
using My.CoachManager.Presentation.Prism.Core.Interactivity;
using My.CoachManager.Presentation.Prism.Core.Navigation;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    public class ShellViewModel : ScreenViewModel
    {
        #region Fields

        private IRegionNavigationJournal _journal;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ShellViewModel"/>.
        /// </summary>
        public ShellViewModel()
        {
            WorkspaceDialogInteractionRequest = new InteractionRequest<IDialog>();
            DialogInteractionRequest = new InteractionRequest<IDialog>();
            NotificationPopupInteractionRequest = new InteractionRequest<INotificationPopup>();

            ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ShowWorkspaceDialogRequestEvent>().Subscribe(OnShowWorkspaceDialogRequested, ThreadOption.UIThread, true);
            ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ShowCustomDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ShowMessageDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ShowNotificationPopupRequestEvent>().Subscribe(OnShowNotificationRequested, ThreadOption.UIThread, true);
            ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<ShowLoginDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<NotifyNavigationCompletedEvent>().Subscribe(OnNavigateCompleted, ThreadOption.UIThread, true);

            var navigateCommand = new DelegateCommand<string>(Navigate, s => true);

            GlobalCommands.NavigateCommand.RegisterCommand(navigateCommand);

            GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);

            IsMenuExpended = true;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the menu is expended.
        /// </summary>
        public bool IsMenuExpended { get; set; }

        /// <summary>
        /// Gets or sets the active workspace.
        /// </summary>
        public INavigatableWorkspaceViewModel ActiveWorkspace { get; private set; }

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
            var splitPath = navigatePath.Split('?');
            var path = splitPath[0];

            if (splitPath.Length > 1)
            {
                var parameters = new NavigationParameters(splitPath[1]);
                ServiceLocator.Current.TryResolve<INavigationService>().NavigateTo(path, parameters.Select(x => new KeyValuePair<string, object>(x.Key, x.Value)));
            }
            else
            {
                ServiceLocator.Current.TryResolve<INavigationService>().NavigateTo(path);
            }
        }

        /// <summary>
        /// Call when the navigation is completed.
        /// </summary>
        /// <param name="e"></param>
        private void OnNavigateCompleted(NavigationCompletedEventArgs e)
        {
            _journal = e.Context.NavigationService.Journal;
            ActiveWorkspace = e.Workspace;

            GoForwardCommand.RaiseCanExecuteChanged();
            GoBackCommand.RaiseCanExecuteChanged();

            if (ActiveWorkspace != null)
            {
                //ServiceLocator.Current.TryResolve<IEventAggregator>().GetEvent<UpdateStatusBarMessageRequestEvent>().Publish(ActiveWorkspace.Title);
            }
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        private void OnShowWorkspaceDialogRequested(DialogEventArgs e)
        {
            WorkspaceDialogInteractionRequest.Raise(e.Dialog, e.Callback);
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        private void OnShowDialogRequested(DialogEventArgs e)
        {
            DialogInteractionRequest.Raise(e.Dialog, e.Callback);
        }

        /// <summary>
        /// Call when the window dialog is requested.
        /// </summary>
        /// <param name="e"></param>
        private void OnShowNotificationRequested(NotificationEventArgs e)
        {
            NotificationPopupInteractionRequest.Raise(e.Notification, e.Callback);
        }

        #region GoBack

        /// <summary>
        /// Go back.
        /// </summary>
        private void GoBack()
        {
            _journal?.GoBack();
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private bool CanGoBack()
        {
            return _journal?.CanGoBack ?? false;
        }

        #endregion GoBack

        #region GoForward

        /// <summary>
        /// Go back.
        /// </summary>
        private void GoForward()
        {
            _journal?.GoForward();
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private bool CanGoForward()
        {
            return _journal?.CanGoForward ?? false;
        }

        #endregion GoForward

        #endregion Methods
    }
}