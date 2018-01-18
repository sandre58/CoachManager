using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PlayerPositionMetadata))]
    public class PlayerPositionViewModel : EntityViewModelBase
    {
        private int? _playerId;

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int? PlayerId { get { return _playerId; } set { SetProperty(ref _playerId, value); } }

        private PlayerViewModel _player;

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public PlayerViewModel Player { get { return _player; } set { SetProperty(ref _player, value); } }

        private int? _positionId;

        /// <summary>
        /// Gets or sets the position id.
        /// </summary>
        public int? PositionId { get { return _positionId; } set { SetProperty(ref _positionId, value); } }

        private PositionViewModel _position;

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public PositionViewModel Position { get { return _position; } set { SetProperty(ref _position, value); } }

        private int _rating;

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        public int Rating { get { return _rating; } set { SetProperty(ref _rating, value); } }
    }
}