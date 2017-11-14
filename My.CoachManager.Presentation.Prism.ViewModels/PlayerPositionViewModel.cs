using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PlayerPositionMetadata))]
    public class PlayerPositionViewModel : EntityViewModelBase
    {
        private int? _playerId;
        public int? PlayerId { get { return _playerId; } set { SetProperty(ref _playerId, value); } }

        private PlayerViewModel _player;
        public PlayerViewModel Player { get { return _player; } set { SetProperty(ref _player, value); } }

        private int? _positionId;
        public int? PositionId { get { return _positionId; } set { SetProperty(ref _positionId, value); } }

        private PositionViewModel _position;
        public PositionViewModel Position { get { return _position; } set { SetProperty(ref _position, value); } }

        private bool _natural;
        public bool Natural { get { return _natural; } set { SetProperty(ref _natural, value); } }

        private int _rating;
        public int Rating { get { return _rating; } set { SetProperty(ref _rating, value); } }
    }
}