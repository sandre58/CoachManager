using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(RosterPlayerMetadata))]
    public class RosterPlayerViewModel : EntityViewModelBase
    {
        private int _rosterId;

        /// <summary>
        /// Gets or sets the player's roster id.
        /// </summary>
        public int RosterId { get { return _rosterId; } set { SetProperty(ref _rosterId, value); } }

        private RosterViewModel _roster;

        /// <summary>
        /// Gets or sets the player's roster.
        /// </summary>
        public RosterViewModel Roster { get { return _roster; } set { SetProperty(ref _roster, value); } }

        private int _playerId;

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int PlayerId { get { return _playerId; } set { SetProperty(ref _playerId, value); } }

        private PlayerViewModel _player;

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public PlayerViewModel Player { get { return _player; } set { SetProperty(ref _player, value); } }

        private int _squadId;

        /// <summary>
        /// Gets or sets the player's squad id.
        /// </summary>
        public int SquadId { get { return _squadId; } set { SetProperty(ref _squadId, value); } }

        private SquadViewModel _squad;

        /// <summary>
        /// Gets or sets the player's squad.
        /// </summary>
        public SquadViewModel Squad { get { return _squad; } set { SetProperty(ref _squad, value); } }

        private int? _number;

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        public int? Number { get { return _number; } set { SetProperty(ref _number, value); } }

        private LicenseState _licenseState;

        /// <summary>
        /// Gets or sets the license state.
        /// </summary>
        public LicenseState LicenseState { get { return _licenseState; } set { SetProperty(ref _licenseState, value); } }

        private bool _isMutation;

        /// <summary>
        /// Gets or sets a value indicates if the player is in mutation.
        /// </summary>
        public bool IsMutation { get { return _isMutation; } set { SetProperty(ref _isMutation, value); } }
    }
}