using Microsoft.Practices.ServiceLocation;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Presentation.Prism.Core.Commands;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Events;
using My.CoachManager.Presentation.Prism.Core.Helpers;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Modules.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Core.Views;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.Wpf.Resources;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using Prism.Events;
using Prism.Interactivity.InteractionRequest;
using Prism.Regions;
using System.Collections.Generic;
using System.Windows;

namespace My.CoachManager.Presentation.Prism.Wpf.ViewModels
{
    /// <summary>
    /// Shell view model.
    /// </summary>
    public class ShellViewModel : ScreenViewModel
    {
        #region Fields

        private readonly IRosterService _rosterService;

        #endregion Fields

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
        /// Gets or sets the set roster command.
        /// </summary>
        public DelegateCommand SetRosterCommand { get; set; }

        /// <summary>
        /// Gets or sets the remove squad command.
        /// </summary>
        public DelegateCommand RemoveSquadCommand { get; set; }

        /// <summary>
        /// Gets or sets the add squad command.
        /// </summary>
        public DelegateCommand AddSquadCommand { get; set; }

        /// <summary>
        /// Gets or sets the edit squad command.
        /// </summary>
        public DelegateCommand<SquadModel> EditSquadCommand { get; set; }

        /// <summary>
        /// Gets or sets selectable rosters.
        /// </summary>
        public RosterModel Roster { get; set; }

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
            SetRosterCommand = new DelegateCommand(SetRoster, CanSetRoster);
            AddSquadCommand = new DelegateCommand(AddSquad, CanAddSquad);
            RemoveSquadCommand = new DelegateCommand(RemoveSquad, CanRemoveSquad);
            EditSquadCommand = new DelegateCommand<SquadModel>(EditSquad, CanEditSquad);
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
            var result = _rosterService.GetRosterById(SettingsManager.GetRosterId());

            Roster = RosterFactory.Get(result);
        }

        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();

            if (Roster.Squads.Count == 0)
            {
                AddSquad();
            }
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

        #region SetRoster

        /// <summary>
        /// Go back.
        /// </summary>
        private void SetRoster()
        {
            DialogManager.ShowSelectItemsDialog<SelectRostersView>(dialog =>
            {
                var model = dialog.Content.DataContext as ISelectItemsViewModel<RosterModel>;

                if (dialog.Result == DialogResult.Ok)
                {
                    if (model != null)
                    {
                        SettingsManager.SaveRoster(model.SelectedItem.Id);
                        ApplicationHelper.Restart();
                    }
                }
            },
            SelectionMode.Single,
            new List<RosterModel> {
                new RosterModel
                {
                    Id = SettingsManager.GetRosterId()
                }});
        }

        /// <summary>
        /// Can go back.
        /// </summary>
        /// <returns></returns>
        private bool CanSetRoster()
        {
            return true;
        }

        #endregion SetRoster

        #region AddSquad

        /// <summary>
        /// Add squad.
        /// </summary>
        private void AddSquad()
        {
            DialogManager.ShowEditDialog<SquadEditView>(0, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    ApplicationHelper.Restart();
                }
            });
        }

        /// <summary>
        /// Can add squad.
        /// </summary>
        /// <returns></returns>
        private bool CanAddSquad()
        {
            return true;
        }

        #endregion AddSquad

        #region EditSquad

        /// <summary>
        /// Edit squad.
        /// </summary>
        private void EditSquad(SquadModel item)
        {
            DialogManager.ShowEditDialog<SquadEditView>(item.Id, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    ApplicationHelper.Restart();
                }
            });
        }

        /// <summary>
        /// Can edit squad.
        /// </summary>
        /// <returns></returns>
        private bool CanEditSquad(SquadModel item)
        {
            return true;
        }

        #endregion EditSquad

        #region RemoveSquad

        /// <summary>
        /// Remove squad.
        /// </summary>
        private void RemoveSquad()
        {
            var view = ServiceLocator.Current.GetInstance<SelectSquadsView>();
            if (view.DataContext is SelectSquadsViewModel model)
            {
                model.RosterId = Roster.Id;

                DialogManager.ShowSelectItemsDialog(view, dialog =>
                {
                    if (dialog.Result == DialogResult.Ok)
                    {
                        if (Roster.Squads.Count > 1)
                        {
                            if (DialogManager.ShowWarningDialog(MenuResources.ConfirmationRemoveSquadMessage, MessageDialogButtons.YesNo) ==
                                DialogResult.Yes)
                            {
                                _rosterService.RemoveSquad(SquadFactory.Get(model.SelectedItem, CrudStatus.Deleted));
                                ApplicationHelper.Restart();
                            }
                        }
                        else
                        {
                            NotificationManager.ShowError(MenuResources.RemoveSquadCountMessage);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Can remove squad.
        /// </summary>
        /// <returns></returns>
        private bool CanRemoveSquad()
        {
            return Roster != null && Roster.Squads.Count > 1;
        }

        #endregion RemoveSquad

        #endregion Methods
    }
}