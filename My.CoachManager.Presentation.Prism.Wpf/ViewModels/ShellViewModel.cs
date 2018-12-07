using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Commands;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Events;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
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

        private readonly IRosterService _rosterService;

        #endregion

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ShellViewModel"/>.
        /// </summary>
        public ShellViewModel(IRosterService rosterService)
        {
            _rosterService = rosterService;
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
        public INavigatableWorkspaceViewModel ActiveWorkspace => (NavigationManager.ActiveView as FrameworkElement)?.DataContext as INavigatableWorkspaceViewModel;

        /// <summary>
        /// Gets or sets the active workspace dialog.
        /// </summary>
        public IWorkspaceDialogViewModel ActiveWorkspaceDialog { get; private set; }

        /// <summary>
        /// Gets or Set the dialog interraction.
        /// </summary>
        public InteractionRequest<IWorkspaceDialog> WorkspaceDialogInteractionRequest { get; set; }

        /// <summary>
        /// Gets or sets the go back command.
        /// </summary>
        public DelegateCommand GoBackCommand { get; set; }

        /// <summary>
        /// Gets or sets the go forward command.
        /// </summary>
        public DelegateCommand GoForwardCommand { get; set; }

        /// <summary>
        /// Gets or sets selectable rosters.
        /// </summary>
        public ObservableCollection<RosterModel> Rosters { get; set; }

        /// <summary>
        /// Gets or sets selected Roster.
        /// </summary>
        public RosterModel SelectedRoster { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets if we can refresh after initialisation.
        /// </summary>
        public override bool RefreshOnInit => true;

        #endregion Members

        #region Initialisation

        /// <inheritdoc />
        /// <summary>
        /// Launch on constructor for initialize all command property.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            var navigateCommand = new DelegateCommand<string>(s => NavigationManager.Navigate(s));

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

            WorkspaceDialogInteractionRequest = new InteractionRequest<IWorkspaceDialog>();
            WorkspaceDialogInteractionRequest.Raised += OnWorkspaceDialogInteractionRequestRaised;

            NavigationManager.Navigated += OnNavigateCompleted;
        }

        #endregion Initialisation

        #region Methods

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _rosterService.GetRosters();

            Rosters = result.Select(RosterFactory.Get).ToObservableCollection();
        }

        #endregion Data

        private void OnWorkspaceDialogInteractionRequestRaised(object sender, InteractionRequestedEventArgs e)
        {
            ActiveWorkspaceDialog = (e.Context.Content as FrameworkElement)?.DataContext as IWorkspaceDialogViewModel;
            if (ActiveWorkspaceDialog != null)
                ActiveWorkspaceDialog.CloseRequest += (o, args) =>
                {
                    ActiveWorkspaceDialog = null;
                };
        }

        /// <summary>
        /// Call when the navigation is completed.
        /// </summary>
        private void OnNavigateCompleted(object sender, RegionNavigationEventArgs e)
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