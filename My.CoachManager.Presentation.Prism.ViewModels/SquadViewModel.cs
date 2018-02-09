using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Data Transfer Object for Squad item.
    /// </summary>
    [MetadataType(typeof(SquadMetadata))]
    public class SquadViewModel : EntityViewModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="SquadViewModel"/>.
        /// </summary>
        public SquadViewModel()
        {
            Players = new ObservableCollection<SquadPlayerViewModel>();
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

        private int _rosterId;

        /// <summary>
        /// Gets or sets the roster Id.
        /// </summary>
        public int RosterId
        {
            get { return _rosterId; }
            set { SetProperty(ref _rosterId, value); }
        }

        private RosterViewModel _roster;

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        public RosterViewModel Roster
        {
            get { return _roster; }
            set { SetProperty(ref _roster, value); }
        }

        private ObservableCollection<SquadPlayerViewModel> _players;

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ObservableCollection<SquadPlayerViewModel> Players
        {
            get { return _players; }
            set { SetProperty(ref _players, value); }
        }
    }
}