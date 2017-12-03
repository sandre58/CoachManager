using System.Collections.Generic;
using System.Collections.ObjectModel;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.RosterModule.Enums;
using My.CoachManager.Presentation.Prism.RosterModule.Resources.Strings;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.RosterServiceReference;
using Prism.Commands;
using Prism.Events;

namespace My.CoachManager.Presentation.Prism.RosterModule.ViewModels
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

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IRosterService rosterService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            _rosterService = rosterService;

            Title = RosterResources.PlayersTitle;

            PresetColumns = new Dictionary<PresetColumnsType, string[]>();
            PresetColumns.Add(PresetColumnsType.GeneralInformations, GeneralInformationsColumns);
            PresetColumns.Add(PresetColumnsType.ClubInformations, ClubInformationsColumns);
            PresetColumns.Add(PresetColumnsType.BodyInformations, BodyInformationsColumns);

            ChangeDisplayedColumnsCommand = new DelegateCommand<PresetColumnsType?>(ChangeDisplayedColumns);
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore(bool isFirstLoading = false)
        {
            var result = _rosterService.GetPlayers(1);

            Items = new ObservableCollection<PlayerDetailViewModel>(result.ToViewModels<PlayerDetailViewModel>());
        }

        /// <summary>
        /// Calls after load data.
        /// </summary>
        protected override void AfterLoadData(bool isFirstLoading = false)
        {
            ChangeDisplayedColumns(PresetColumnsType.GeneralInformations);
            base.AfterLoadData(isFirstLoading);
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
    }
}