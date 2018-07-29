using System.Windows;
using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.Commands;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    /// <summary>
    /// Shell view model.
    /// </summary>
    public class ShellViewModel : ScreenViewModel
    {
        #region Fields

        private INavigationService _navigationService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the menu is expended.
        /// </summary>
        public bool IsMenuExpended { get; set; }

        /// <summary>
        /// Gets or sets the active workspace.
        /// </summary>
        public INavigatableWorkspaceViewModel ActiveWorkspace => (NavigationService.ActiveView as FrameworkElement)?.DataContext as INavigatableWorkspaceViewModel;

        /// <summary>
        /// Gets the navigation service.
        /// </summary>
        private INavigationService NavigationService => _navigationService ?? (_navigationService = ServiceLocator.Current.TryResolve<INavigationService>());

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

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            var navigateCommand = new DelegateCommand<string>(s => NavigationManager.Navigate(s, OnNavigateCompleted));

            GlobalCommands.NavigateCommand.RegisterCommand(navigateCommand);

            GoBackCommand = new DelegateCommand(GoBack, CanGoBack);
            GoForwardCommand = new DelegateCommand(GoForward, CanGoForward);
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Data.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            WorkspaceDialogInteractionRequest = new InteractionRequest<IDialog>();
            DialogInteractionRequest = new InteractionRequest<IDialog>();
            NotificationPopupInteractionRequest = new InteractionRequest<INotificationPopup>();

            IsMenuExpended = true;
        }

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all Event.
        /// </summary>
        protected override void InitializeEvent()
        {
            base.InitializeEvent();

            EventAggregator.GetEvent<ShowWorkspaceDialogRequestEvent>().Subscribe(OnShowWorkspaceDialogRequested, ThreadOption.UIThread, true);
            EventAggregator.GetEvent<ShowCustomDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            EventAggregator.GetEvent<ShowMessageDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
            EventAggregator.GetEvent<ShowNotificationPopupRequestEvent>().Subscribe(OnShowNotificationRequested, ThreadOption.UIThread, true);
            EventAggregator.GetEvent<ShowLoginDialogRequestEvent>().Subscribe(OnShowDialogRequested, ThreadOption.UIThread, true);
        }

        #endregion Initialisation

        #region Methods

        /// <summary>
        /// Call when the navigation is completed.
        /// </summary>
        /// <param name="e"></param>
        private void OnNavigateCompleted(NavigationResult e)
        {
            RaisePropertyChanged(nameof(ActiveWorkspace));
            GoForwardCommand.RaiseCanExecuteChanged();
            GoBackCommand.RaiseCanExecuteChanged();
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
            NavigationManager.GoBack();
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private bool CanGoBack()
        {
            return NavigationManager.CanGoBack();
        }

        #endregion GoBack

        #region GoForward

        /// <summary>
        /// Go back.
        /// </summary>
        private void GoForward()
        {
            NavigationManager.GoForward();
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private bool CanGoForward()
        {
            return NavigationManager.CanGoForward();
        }

        #endregion GoForward

        #endregion Methods
    }
}