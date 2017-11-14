using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PlayerWeightMetadata))]
    public class PlayerWeightViewModel : EntityViewModelBase
    {
        private int? _playerId;
        public int? PlayerId { get { return _playerId; } set { SetProperty(ref _playerId, value); } }

        private PlayerViewModel _player;
        public PlayerViewModel Player { get { return _player; } set { SetProperty(ref _player, value); } }

        private int _value;
        public int Value { get { return _value; } set { SetProperty(ref _value, value); } }

        private DateTime _date;
        public DateTime Date { get { return _date; } set { SetProperty(ref _date, value); } }
    }
}