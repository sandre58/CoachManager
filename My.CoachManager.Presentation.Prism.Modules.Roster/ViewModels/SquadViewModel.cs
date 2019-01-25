using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.Prism.Models.Filters;
using My.CoachManager.Presentation.Prism.Modules.Core.Views;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using My.CoachManager.Presentation.Prism.Modules.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Modules.Roster.Enums;
using SquadResources = My.CoachManager.Presentation.Prism.Modules.Roster.Resources.SquadResources;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class SquadViewModel : ListViewModel<RosterPlayerModel, RosterPlayerEditView, RosterPlayerView>
    {
        #region Fields

        private readonly IRosterService _rosterService;
        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private readonly IPositionService _positionService;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets model.
        /// </summary>
        public RosterModel Roster { get; set; }

        /// <summary>
        /// Gets or sets model.
        /// </summary>
        public SquadModel Squad { get; set; }

        /// <summary>
        ///
        /// </summary>
        public bool CanMoveToSquadSelectedItems => SelectedItems.Any() && Roster.Squads.Count > 1;

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public DelegateCommand AddExistingPlayersCommand { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<SquadModel> SquadsOfSelectedItemsToMoving => Roster?.Squads.Where(squad => SelectedItems.Select(player => player.SquadId).Any(playerSquadId => playerSquadId != squad.Id)).ToList();

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public DelegateCommand<SquadModel> MoveSelectedPlayersInSquadCommand { get; set; }

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public DelegateCommand<List<object>> MovePlayerInSquadCommand { get; set; }

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public DelegateCommand<RosterPlayerModel> OpenItemInInjuryTabCommand { get; set; }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="SquadViewModel"/>.
        /// </summary>
        public SquadViewModel(IRosterService rosterService, IPersonService personService, ICategoryService categoryService, IPositionService positionService)
        {
            _rosterService = rosterService;
            _personService = personService;
            _categoryService = categoryService;
            _positionService = positionService;
            ConfirmationRemoveItemMessage = SquadResources.ConfirmationRemovingItemMessage;
            ConfirmationRemoveItemsMessage = SquadResources.ConfirmationRemovingItemsMessage;
        }

        #endregion Constructors

        #region Methods

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            MoveSelectedPlayersInSquadCommand = new DelegateCommand<SquadModel>(MoveSelectedPlayersInSquad, CanMoveSelectedPlayersInSquad);
            MovePlayerInSquadCommand = new DelegateCommand<List<object>>(MovePlayerInSquad);
            AddExistingPlayersCommand = new DelegateCommand(AddExistingPlayers);
            OpenItemInInjuryTabCommand = new DelegateCommand<RosterPlayerModel>(OpenItemInInjuryTab, CanOpenItemInInjuryTab);
        }

        #endregion Initialization

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Remove the item from data source.
        /// </summary>
        /// <param name="item"></param>
        protected override void RemoveItemCore(RosterPlayerModel item)
        {
            _rosterService.RemovePlayers(Roster.Id, new[] { item.PlayerId });
        }

        /// <inheritdoc />
        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            if (NavigationId > 0)
            {
                Squad = SquadFactory.Get(_rosterService.GetSquadById(NavigationId));
                Roster = RosterFactory.Get(_rosterService.GetRosterById(Thread.CurrentPrincipal.Identity.GetRosterId()));
                var result = _rosterService.GetPlayers(Roster.Id);

                Items = result.Select(RosterFactory.Get).ToItemsObservableCollection();
            }
        }

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            if (NavigationId > 0)
            {
                var roster = RosterFactory.Get(_rosterService.GetRosterFromSquad(NavigationId));

                var categories = _categoryService.GetCategories().Select(CategoryFactory.Get);
                var countries = _personService.GetCountries().Select(CountryFactory.Get);
                var positions = _positionService.GetPositions().Select(PositionFactory.Get);
                var squads = _rosterService.GetSquads(roster.Id).Select(SquadFactory.Get);

                Filters = new SquadFiltersViewModel(categories, positions, countries, squads);

                if (Filters.AllowedFilters.First(x => x.Item1.Invoke().PropertyName == "SquadId").Item1.Invoke() is SquadsFilter squadFilter)
                {
                    System.Windows.Application.Current.Dispatcher.Invoke(() =>
                    {
                        Filters.AddFilter(squadFilter, PlayerResources.Squad);

                        squadFilter.Values = new List<int>
                        {
                            NavigationId
                        };
                    });
                }

                Parameters = new SquadParametersViewModel();
            }
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            base.OnLoadDataCompleted();

            if (Squad != null)
                Title = Squad.Name;
        }

        #endregion Data

        #region Add

        /// <summary>
        /// Add a new item.
        /// </summary>
        protected override void Add()
        {
            DialogManager.ShowEditDialog<PlayerEditView>(ItemParameters.New, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    if (dialog.Content.DataContext is PlayerEditViewModel model)
                    {
                        _rosterService.AddPlayers(Squad.Id, new[] {model.Item.Id});
                        NotificationManager.ShowSuccess(string.Format(MessageResources.ItemsAdded, 1));
                    }
                }

                OnAddCompleted(dialog.Result);
            });
        }

        /// <summary>
        /// Add existing players.
        /// </summary>
        protected void AddExistingPlayers()
        {
            DialogManager.ShowSelectItemsDialog<SelectPlayersView>(dialog =>
                {
                    var model = dialog.Content.DataContext as ISelectItemsViewModel<PlayerModel>;
                    if (dialog.Result == DialogResult.Ok)
                    {
                        if (model != null)
                        {
                            _rosterService.AddPlayers(Squad.Id, model.SelectedItems.Select(x => x.Id).ToArray());

                            NotificationManager.ShowSuccess(string.Format(MessageResources.ItemsAdded,
                                model.SelectedItems.Count()));
                        }
                    }

                    OnAddCompleted(dialog.Result);
                },
                SelectionMode.Multiple,
                Items.Select(x => new PlayerModel()
                {
                    Id = x.PlayerId
                }).ToList());
        }

        #endregion Add

        #region MovePlayerInSquad

        /// <summary>
        /// Move selected Players in a squad.
        /// </summary>
        /// <param name="squad"></param>
        protected void MoveSelectedPlayersInSquad(SquadModel squad)
        {
            if (squad != null)
            {
                _rosterService.MovePlayersInSquad(squad.Id, SelectedItems.Select(x => x.Id).ToArray());
                NotificationManager.ShowSuccess(string.Format(MessageResources.MovingPlayersToSquad, SelectedItems.Count(), squad.Name));
                Refresh();
            }
        }

        /// <summary>
        /// Can move player in squad.
        /// </summary>
        protected bool CanMoveSelectedPlayersInSquad(SquadModel squadId)
        {
            return SelectedItems.Any();
        }

        #endregion MovePlayerInSquad

        #region MovePlayerInSquad

        /// <summary>
        /// Move selected Players in a squad.
        /// </summary>
        /// <param name="values"></param>
        protected void MovePlayerInSquad(List<object> values)
        {
            if (values != null && values.Count == 2)
            {
                if (values[0] is RosterPlayerModel player && values[1] is SquadModel squad)
                {
                    _rosterService.MovePlayersInSquad(squad.Id, new[] { player.Id });
                    NotificationManager.ShowSuccess(string.Format(MessageResources.MovingPlayerToSquad, player.FullName, squad.Name));
                    Refresh();
                }
            }
        }

        #endregion MovePlayerInSquad

        #region OpenItemInInjuryTab

        /// <summary>
        /// Open Item.
        /// </summary>
        protected virtual void OpenItemInInjuryTab(RosterPlayerModel item)
        {
            NavigationManager.NavigateTo<RosterPlayerView>(null, new List<KeyValuePair<string, object>>
            {
                new KeyValuePair<string, object>("Id", item.Id),
                new KeyValuePair<string, object>("Tab", (int)RosterPlayerViewTab.Injuries)
            });
        }

        /// <summary>
        /// Can Open item.
        /// </summary>
        protected virtual bool CanOpenItemInInjuryTab(RosterPlayerModel item)
        {
            return Mode == ScreenMode.Read && item != null;
        }

        #endregion

        #region Properties Changed

        protected override void OnSelectionChanged()
        {
            base.OnSelectionChanged();

            RaisePropertyChanged(() => CanMoveToSquadSelectedItems);
            RaisePropertyChanged(() => SquadsOfSelectedItemsToMoving);
        }

        #endregion Properties Changed

        #endregion Methods
    }
}