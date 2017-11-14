using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Core.ViewModels;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(ContactMetadata))]
    public abstract class ContactViewModel : EntityViewModel, ICollectionnable
    {
        private string _label;
        public virtual string Label { get { return _label; } set { SetEntityProperty(() => _label = value, value, Label); } }

        private bool _default;
        public virtual bool Default { get { return _default; } set { SetEntityProperty(() => _default = value, value, Default); } }

        private int _playerId;
        public virtual int PlayerId { get { return _playerId; } set { SetEntityProperty(() => _playerId = value, value, PlayerId); } }

        private PlayerViewModel _player;
        public virtual PlayerViewModel Player { get { return _player; } set { SetEntityProperty(() => _player = value, value, Player); } }

        private bool _canAdd;
        public virtual bool CanAdd { get { return _canAdd; } set { SetEntityProperty(() => _canAdd = value, value, CanAdd); } }

        private bool _canRemove;
        public virtual bool CanRemove { get { return _canRemove; } set { SetEntityProperty(() => _canRemove = value, value, CanRemove); } }

        public abstract string Value { get; set; }
    }
}