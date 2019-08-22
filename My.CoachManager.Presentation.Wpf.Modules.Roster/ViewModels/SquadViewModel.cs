using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Parameters;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Helpers;
using My.CoachManager.Presentation.Models;
using My.CoachManager.Presentation.Models.Aggregates;
using My.CoachManager.Presentation.Models.Filters;
using My.CoachManager.Presentation.Wpf.Core.Dialog;
using My.CoachManager.Presentation.Wpf.Core.Manager;
using My.CoachManager.Presentation.Wpf.Core.ViewModels;
using My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces;
using My.CoachManager.Presentation.Wpf.Modules.Shared;
using My.CoachManager.Presentation.Wpf.Modules.Shared.Events;
using Prism.Commands;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.ViewModels
{
    public class SquadViewModel : ListViewModel<RosterPlayerModel, RosterPlayerEditViewModel, RosterPlayerViewModel>
    {
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
        public bool CanMoveToSquadSelectedItems => SelectedItems != null && SelectedItems.Any() && Roster.Squads.Count > 1;

        /// <summary>
        /// Gets or sets command.
        /// </summary>
        public DelegateCommand AddExistingPlayersCommand { get; set; }

        /// <summary>
        ///
        /// </summary>
        public List<SquadModel> SquadsOfSelectedItemsToMoving => Roster?.Squads.Where(squad => SelectedItems != null && SelectedItems.Select(player => player.SquadId).Any(playerSquadId => playerSquadId != squad.Id)).ToList();

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

        #region Initialization

        /// <inheritdoc />
        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();
            
            Filters = new SquadFiltersViewModel();
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
            ApiHelper.PostData(ApiConstants.ApiRostersPlayersDelete, new RosterParametersDto { RosterId = Roster.Id, PlayersId = new List<int> { item.PlayerId } });
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
                Roster = AppManager.Roster;
                Squad = SquadFactory.Get(ApiHelper.GetData<SquadDto>(ApiConstants.ApiSquads, NavigationId));
                Title = Squad.Name;

                var result = ApiHelper.GetData<IList<RosterPlayerDto>>(ApiConstants.ApiRostersPlayers, AppManager.Roster.Id);

                Items = result.Select(RosterFactory.Get).ToItemsObservableCollection();
            }
        }

        /// <summary>
        /// Initialize data asynchronous.
        /// </summary>
        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            if (NavigationId > 0)
            {
                var countries = ApiHelper.GetData<IEnumerable<CountryDto>>(ApiConstants.ApiCountries).Select(CountryFactory.Get).ToList();
                var positions = ApiHelper.GetData<IEnumerable<PositionDto>>(ApiConstants.ApiPositions).Select(PositionFactory.Get).ToList();
                var categories = ApiHelper.GetData<IEnumerable<CategoryDto>>(ApiConstants.ApiCategories).Select(CategoryFactory.Get).ToList();

                Filters = new SquadFiltersViewModel(categories, positions, countries, AppManager.Roster.Squads);

                var filter = new SquadsFilter("SquadId", AppManager.Roster.Squads) { Values = new List<int> { NavigationId } };
                Filters.DefaultFilters.Add(new FilterViewModel(filter, PlayerResources.Squad));

                ListParameters = new SquadParametersViewModel();
            }
        }

        #endregion Data

        #region Add

        /// <summary>
        /// Remove Data.
        /// </summary>
        private void Add(int squadId, IList<int> ids)
        {
            CallWebService(() => AddCore(squadId, ids),
                () => OnAddSucceeded(ids),
                null,
                null,
                true);
        }

        /// <summary>
        /// Called when [background worker on do work].
        /// </summary>
        private void AddCore(int squadId, IEnumerable<int> ids)
        {
            ApiHelper.PostData(ApiConstants.ApiRostersPlayersAdd, new RosterParametersDto
            {
                SquadId = squadId,
                PlayersId = ids
            });
        }

        /// <summary>
        /// Called after the edit action;
        /// </summary>
        protected virtual void OnAddSucceeded(IList<int> ids)
        {
            var message = ids.Count > 1 ? string.Format(MessageResources.ItemsAdded, ids.Count) : MessageResources.ItemAdded;
            NotificationManager.ShowSuccess(message);
        }

        /// <inheritdoc />
        /// <summary>
        /// Add a new item.
        /// </summary>
        protected override void Add()
        {
            EventAggregator.GetEvent<EditPlayerRequestEvent>().Publish(new EditItemRequestEventArgs(0, dialog =>
            {
                if (dialog.Result == DialogResult.Ok)
                {
                    if (dialog.Content is IEditViewModel model)
                    {
                        Add(Squad.Id, new List<int> { model.Item.Id });
                    }
                }
            })
                );
        }

        /// <summary>
        /// Add existing players.
        /// </summary>
        private void AddExistingPlayers()
        {
            EventAggregator.GetEvent<SelectPlayersRequestEvent>().Publish(new SelectItemsRequestEventArgs(
                dialog =>
                {
                    if (dialog.Result == DialogResult.Ok)
                    {
                        if (dialog.Content is ISelectItemsViewModel<PlayerModel> model)
                        {
                            Add(Squad.Id, model.SelectedItems.Select(x => x.Id).ToArray());
                        }
                    }
                },
                Core.Enums.SelectionMode.Multiple,
                Items.Select(x => new PlayerModel()
                {
                    Id = x.PlayerId
                }).ToList()
                )
            );
        }

        #endregion Add

        #region MoveSelectedPlayersInSquad

        /// <summary>
        /// Move selected players in squad.
        /// </summary>
        /// <param name="squad"></param>
        /// <param name="players"></param>
        /// <returns></returns>
        private void MovePlayersInSquadCore(SquadModel squad, IList<RosterPlayerModel> players)
        {
            ApiHelper.PostData(ApiConstants.ApiRostersMove,
                new RosterParametersDto { SquadId = squad.Id, PlayersId = players.Select(x => x.Id).ToArray() });
        }

        /// <summary>
        /// Move selected Players in a squad.
        /// </summary>
        /// <param name="squad"></param>
        private void MoveSelectedPlayersInSquad(SquadModel squad)
        {
            MovePlayersInSquad(squad, SelectedItems.ToList());
        }

        /// <summary>
        /// Move selected Players in a squad.
        /// </summary>
        /// <param name="squad"></param>
        /// <param name="players"></param>
        private void MovePlayersInSquad(SquadModel squad, IList<RosterPlayerModel> players)
        {
            if (squad != null)
            {
                CallWebService(() => MovePlayersInSquadCore(squad, players),
                    () =>
                    {
                        if (players.Count == 1)
                            NotificationManager.ShowSuccess(string.Format(MessageResources.MovingPlayerToSquad, players.First().FullName, squad.Name));
                        else if (players.Count > 1)
                        {
                            NotificationManager.ShowSuccess(string.Format(MessageResources.MovingPlayersToSquad, players.Count, squad.Name));
                        }
                    },
                    null,
                    null,
                    true);
            }
        }

        /// <summary>
        /// Can move player in squad.
        /// </summary>
        private bool CanMoveSelectedPlayersInSquad(SquadModel squadId)
        {
            return SelectedItems.Any();
        }

        #endregion MoveSelectedPlayersInSquad

        #region MovePlayerInSquad

        /// <summary>
        /// Move selected Players in a squad.
        /// </summary>
        /// <param name="values"></param>
        private void MovePlayerInSquad(List<object> values)
        {
            if (values != null && values.Count == 2)
            {
                if (values[0] is RosterPlayerModel player && values[1] is SquadModel squad)
                {
                    MovePlayersInSquad(squad, new List<RosterPlayerModel> { player });
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
            //NavigationManager.NavigateTo<RosterPlayerView>(null, new List<KeyValuePair<string, object>>
            //{
            //    new KeyValuePair<string, object>("Id", item.Id),
            //    new KeyValuePair<string, object>("Tab", (int)RosterPlayerViewTab.Injuries)
            //});
        }

        /// <summary>
        /// Can Open item.
        /// </summary>
        protected virtual bool CanOpenItemInInjuryTab(RosterPlayerModel item)
        {
            return Mode == ScreenMode.Read && item != null;
        }

        #endregion OpenItemInInjuryTab

        #region Properties Changed

        protected override void OnSelectionChanged()
        {
            base.OnSelectionChanged();

            RaisePropertyChanged(() => CanMoveToSquadSelectedItems);
            RaisePropertyChanged(() => SquadsOfSelectedItemsToMoving);
        }

        #endregion Properties Changed
    }
}