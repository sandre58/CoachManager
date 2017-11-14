using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(ContactMetadata))]
    public abstract class ContactViewModel : EntityViewModel, ICollectionnable
    {
        private string _label;
        public virtual string Label { get { return _label; } set { SetProperty(ref _label, value); } }

        private bool _default;
        public virtual bool Default { get { return _default; } set { SetProperty(ref _default, value); } }

        private string _value;
        public virtual string Value { get { return _value; } set { SetProperty(ref _value, value); } }

        private bool _canAdd;
        public virtual bool CanAdd { get { return _canAdd; } set { SetProperty(ref _canAdd, value); } }

        private bool _canRemove;
        public virtual bool CanRemove { get { return _canRemove; } set { SetProperty(ref _canRemove, value); } }
    }
}