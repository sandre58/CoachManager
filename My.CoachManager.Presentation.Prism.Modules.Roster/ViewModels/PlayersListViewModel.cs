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
        private IEnumerable<CategoryViewModel> _allCategories;
        private IEnumerable<CountryViewModel> _allCountries;
        private IEnumerable<CityViewModel> _allCities;

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
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryViewModel> AllCategories
        {
            get { return _allCategories; }
            set { SetProperty(ref _allCategories, value); }
        }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public IEnumerable<CountryViewModel> AllCountries
        {
            get { return _allCountries; }
            set
            {
                SetProperty(ref _allCountries, value);
            }
        }

        /// <summary>
        /// Gets or sets cities.
        /// </summary>
        public IEnumerable<CityViewModel> AllCities
        {
            get { return _allCities; }
            set
            {
                SetProperty(ref _allCities, value);
            }
        }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IRosterService rosterService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger, IFiltersViewModel filters)
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

        #endregion Methods

        private IFiltersViewModel _filters;

        private StringFilter _speedFilter;

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

        public DelegateCommand ShowFiltersCommand { get; set; }

        private void ShowFilter()
        {
            DialogService.ShowWorkspaceDialog<FiltersView>(before =>
                {
                },
                after =>
                {
                });
        }
    }
}