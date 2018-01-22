using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Roster.Enums;
using My.CoachManager.Presentation.Prism.Modules.Roster.Resources.Strings;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public class PlayersListViewModel : ReadOnlyListViewModel<PlayerDetailViewModel>, IPlayersListViewModel
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Number", "Category", "License", "LicenseState" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Fields

        private readonly IRosterService _rosterService;
        private ObservableCollection<string> _displayedColumns;
        private Dictionary<PresetColumnsType, string[]> _presetColumns;
        private IFiltersViewModel _filters;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the columns to displayed.
        /// </summary>
        public ObservableCollection<string> DisplayedColumns
        {
            get { return _displayedColumns; }
            set { SetProperty(ref _displayedColumns, value); }
        }

        /// <summary>
        /// Gets or sets the preset columns to displayed.
        /// </summary>
        public Dictionary<PresetColumnsType, string[]> PresetColumns
        {
            get { return _presetColumns; }
            set { SetProperty(ref _presetColumns, value); }
        }

        /// <summary>
        /// Command to change displayed columns.
        /// </summary>
        public DelegateCommand<PresetColumnsType?> ChangeDisplayedColumnsCommand { get; set; }

        /// <summary>
        /// Gets or sets Show Filters Command.
        /// </summary>
        public DelegateCommand ShowFiltersCommand { get; set; }

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public IFiltersViewModel Filters
        {
            get { return _filters; }
            set
            {
                SetProperty(ref _filters, value);
            }
        }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IRosterService rosterService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger, IPlayerFiltersViewModel filters)
            : base(dialogService, eventAggregator, logger)
        {
            _rosterService = rosterService;

            Title = RosterResources.PlayersTitle;

            PresetColumns = new Dictionary<PresetColumnsType, string[]>
            {
                {PresetColumnsType.GeneralInformations, GeneralInformationsColumns},
                {PresetColumnsType.ClubInformations, ClubInformationsColumns},
                {PresetColumnsType.BodyInformations, BodyInformationsColumns}
            };

            ShowFiltersCommand = new DelegateCommand(ShowFilter);
            ChangeDisplayedColumnsCommand = new DelegateCommand<PresetColumnsType?>(ChangeDisplayedColumns);

            SpeedFilter = new StringFilter(typeof(PlayerDetailViewModel).GetProperty("FullName"));
            SpeedFilter.PropertyChanged += SpeedFilter_FilteringChanged;
            Filters = filters;

            Filters.FiltersChanged += Filters_FiltersChanged;
        }

        private void Filters_FiltersChanged(object sender, EventArgs e)
        {
            FilteredItems.ChangeFilters(Filters.AvailableFilters);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            var result = _rosterService.GetPlayers(1);

            System.Windows.Application.Current.Dispatcher.Invoke(() =>
            {
                SetCollection(result.ToViewModels<PlayerDetailViewModel>());
            });
        }

        protected override void OnLoadDataCompleted()
        {
            ChangeDisplayedColumns(PresetColumnsType.GeneralInformations);
            base.OnLoadDataCompleted();
        }

        /// <summary>
        /// Changes displayed columns.
        /// </summary>
        protected void ChangeDisplayedColumns(PresetColumnsType? type)
        {
            if (type.HasValue)
                DisplayedColumns = new ObservableCollection<string>(PresetColumns[type.Value]);
        }

        /// <summary>
        /// Show Filters dialog.
        /// </summary>
        private void ShowFilter()
        {
            DialogService.ShowWorkspaceDialog<PlayerFiltersView>();
        }

        #endregion Methods

        private StringFilter _speedFilter;

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public StringFilter SpeedFilter
        {
            get { return _speedFilter; }
            set
            {
                SetProperty(ref _speedFilter, value);
            }
        }

        private void SpeedFilter_FilteringChanged(object sender, EventArgs e)
        {
            if (SpeedFilter.Value != "")
            {
                FilteredItems.AddFilter(SpeedFilter);
            }
            else
            {
                FilteredItems.RemoveFilter(SpeedFilter);
            }
            RaisePropertyChanged(() => FilteredItems);
        }
    }
}