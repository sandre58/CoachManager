using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Contact item.
    /// </summary>
    [MetadataType(typeof(ContactMetadata))]
    public abstract class ContactViewModel : EntityViewModel, ICollectionnable
    {
        private string _label;

        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public virtual string Label { get { return _label; } set { SetProperty(ref _label, value); } }

        private bool _default;

        /// <summary>
        /// Gets or sets a value indicates if this contact is the default contact.
        /// </summary>
        public virtual bool Default { get { return _default; } set { SetProperty(ref _default, value); } }

        private string _value;

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public virtual string Value { get { return _value; } set { SetProperty(ref _value, value); } }

        private int _personId;

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public virtual int PersonId { get { return _personId; } set { SetProperty(ref _personId, value); } }

        private bool _canAdd;

        /// <summary>
        /// Gets or sets a value indicates if we can add a new contact for active person.
        /// </summary>
        public virtual bool CanAdd { get { return _canAdd; } set { SetProperty(ref _canAdd, value); } }

        private bool _canRemove;

        /// <summary>
        /// Gets or sets a value indicates if we can remove a new contact for active person.
        /// </summary>
        public virtual bool CanRemove { get { return _canRemove; } set { SetProperty(ref _canRemove, value); } }
    }
}