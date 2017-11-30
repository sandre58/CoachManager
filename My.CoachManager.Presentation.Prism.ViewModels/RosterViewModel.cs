using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Data Transfer Object for Roster item.
    /// </summary>
    [MetadataType(typeof(RosterMetadata))]
    public class RosterViewModel : EntityViewModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="RosterViewModel"/>.
        /// </summary>
        public RosterViewModel()
        {
            Players = new ObservableCollection<RosterPlayerViewModel>();
            Coachs = new ObservableCollection<RosterCoachViewModel>();
            Squads = new ObservableCollection<SquadViewModel>();
        }

        private string _name;

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name
        {
            get { return _name; }
            set { SetProperty(ref _name, value); }
        }

        private int _seasonId;

        /// <summary>
        /// Gets or sets the season id.
        /// </summary>
        public int SeasonId
        {
            get { return _seasonId; }
            set { SetProperty(ref _seasonId, value); }
        }

        private SeasonViewModel _season;

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        public SeasonViewModel Season
        {
            get { return _season; }
            set { SetProperty(ref _season, value); }
        }

        private int? _categoryId;

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int? CategoryId
        {
            get { return _categoryId; }
            set { SetProperty(ref _categoryId, value); }
        }

        private CategoryViewModel _category;

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public CategoryViewModel Category
        {
            get { return _category; }
            set { SetProperty(ref _category, value); }
        }

        private ObservableCollection<RosterPlayerViewModel> _players;

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ObservableCollection<RosterPlayerViewModel> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }

        private ObservableCollection<RosterCoachViewModel> _coachs;

        /// <summary>
        /// Gets or set the coachs.
        /// </summary>
        public ObservableCollection<RosterCoachViewModel> Coachs
        {
            get { return _coachs; }
            set { SetProperty(ref _coachs, value); }
        }

        private ObservableCollection<SquadViewModel> _squads;

        /// <summary>
        /// Gets or set the squads.
        /// </summary>
        public ObservableCollection<SquadViewModel> Squads
        {
            get { return _squads; }
            set { SetProperty(ref _squads, value); }
        }
    }
}