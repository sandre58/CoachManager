using System.Collections.ObjectModel;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
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
        #region Fields

        private readonly IRosterService _rosterService;
        private ObservableCollection<string> _activeVisibleColumns;
        private ObservableCollection<ObservableCollection<string>> _defaultVisibleColumns;

        public ObservableCollection<string> ActiveVisibleColumns
        {
            get { return _activeVisibleColumns; }
            set { SetProperty(ref _activeVisibleColumns, value); }
        }

        public ObservableCollection<ObservableCollection<string>> DefaultVisibleColumns
        {
            get { return _defaultVisibleColumns; }
            set { SetProperty(ref _defaultVisibleColumns, value); }
        }

        public DelegateCommand<ObservableCollection<string>> TestCommand { get; set; }

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayersListViewModel"/>.
        /// </summary>
        public PlayersListViewModel(IRosterService rosterService, IDialogService dialogService, IEventAggregator eventAggregator, ILogger logger)
            : base(dialogService, eventAggregator, logger)
        {
            _rosterService = rosterService;

            Title = RosterResources.PlayersTitle;

            DefaultVisibleColumns = new ObservableCollection<ObservableCollection<string>>();
            DefaultVisibleColumns.Add(new ObservableCollection<string>(new[] { "birthdate", "country", "category" }));
            DefaultVisibleColumns.Add(new ObservableCollection<string>(new[] { "address", "phone", "email" }));
            DefaultVisibleColumns.Add(new ObservableCollection<string>(new[] { "size", "height", "weight" }));

            TestCommand = new DelegateCommand<ObservableCollection<string>>(col =>
            {
                ActiveVisibleColumns = col;
            });
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

        #endregion Methods
    }
}