using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Controls.Extensions;
using My.CoachManager.Presentation.Prism.Core.Dialog;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Roster.Core.Enums;
using My.CoachManager.Presentation.Prism.Modules.Roster.Views;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using Prism.Events;
using RosterResources = My.CoachManager.Presentation.Prism.Modules.Roster.Core.Resources.Strings.RosterResources;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    [Serializable]
    public class PlayersListViewModel : ReadOnlyListViewModel<PlayerDetailViewModel>, IPlayersListViewModel
    {
        #region Constants

        private static readonly string[] GeneralInformationsColumns =
            {"Age", "Birthdate", "Category", "Gender", "Country", "Address", "Phone", "Email"};

        private static readonly string[] ClubInformationsColumns = { "Number", "Category", "License", "LicenseState" };

        private static readonly string[] BodyInformationsColumns =
            {"Laterality", "Height", "Weight", "Size", "ShoesSize"};

        #endregion Constants

        #region Fields

        private readonly IAdminService _adminService;
        private readonly IRosterService _rosterService;
        private ObservableCollection<string> _displayedColumns;
        private Dictionary<PresetColumnsType, string[]> _presetColumns;
        private IFiltersViewModel _filters;
        private IEnumerable<FilterViewModel> _saveFilters;
        private IEnumerable<CategoryViewModel> _categories;

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
            set { SetProperty(ref _filters, value); }
        }

        /// <summary>
        /// Gets or sets the count filters.
        /// </summary>
        public int CountFilters
        {
            get { return Filters.Filters.Count(x => x.IsEnabled); }
        }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IRosterService rosterService, IAdminService adminService,
            IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger,
            IPlayerFiltersViewModel filters)
            : base(dialogService, eventAggregator, logger)
        {
            _rosterService = rosterService;
            _adminService = adminService;

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
            Filters.FiltersChanged += (sender, args) =>
            {
                if (Filters.UpdateOnLive) OnFiltersChanged(sender, args);
            };
        }

        #endregion Constructors

        #region Methods

        protected override void InitializeDataCore()
        {
            base.InitializeDataCore();

            _categories = _adminService.GetCategoriesList().ToViewModels<CategoryViewModel>();

            Filters.AllowedFilters.Add("FullName", PersonResources.FullName);
            Filters.AllowedFilters.Add("Age", PersonResources.Age);
            Filters.AllowedFilters.Add("Number", PlayerResources.Number);
            Filters.AllowedFilters.Add("CategoryId", PlayerResources.Category);

            Filters.CreateFilter = CreateFilter;
        }

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
            DialogService.ShowWorkspaceDialog<PlayerFiltersView>(before =>
            {
                //if (_saveFilters != null)
                //{
                //    var model = before.Context as IFiltersViewModel;
                //    if (model != null)
                //    {
                //        model.Filters = new ObservableCollection<FilterViewModel>(_saveFilters.Clone2());
                //    }
                //}
            },
                after =>
                {
                    //var model = after.Context as IFiltersViewModel;
                    //if (model != null)
                    //{
                    //    if (after.Result == DialogResult.Ok)
                    //    {
                    //        OnFiltersChanged(this, EventArgs.Empty);
                    //        _saveFilters = model.Filters.Clone2();
                    //    }

                    //    if (after.Result == DialogResult.Cancel && model.UpdateOnLive)
                    //    {
                    //        model.Filters = new ObservableCollection<FilterViewModel>(_saveFilters.Clone2());
                    //        OnFiltersChanged(this, EventArgs.Empty);
                    //    }
                    //}
                });
        }

        private void OnFiltersChanged(object sender, EventArgs e)
        {
            Task.Run(() =>
            {
                System.Windows.Application.Current.Invoke(() =>
                {
                    FilteredItems.ChangeFilters(Filters.Filters.Where(x => x.IsEnabled)
                        .Select(x => new Tuple<LogicalOperator, IFilter>(x.Operator, x.Filter)));
                    RaisePropertyChanged(() => CountFilters);
                });
            });
        }

        /// <summary>
        /// Create a filter.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        private IFilter CreateFilter(string propertyName)
        {
            var property = GetPropertyInfo(propertyName);
            switch (propertyName)
            {
                case "FullName":
                    return new StringFilter(property);

                case "Age":
                    return new IntegerFilter(property, ComparableOperator.IsBetween, 15, 45);

                case "Number":
                    return new IntegerFilter(property, ComparableOperator.EqualsTo, 1, 10);

                case "CategoryId":
                    return new SelectedLabelablesFilter(property, _categories);
            }

            return null;
        }

        #endregion Methods

        private StringFilter _speedFilter;

        /// <summary>
        /// Gets or sets the filter for speed Search.
        /// </summary>
        public StringFilter SpeedFilter
        {
            get { return _speedFilter; }
            set { SetProperty(ref _speedFilter, value); }
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