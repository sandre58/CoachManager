using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Contact item.
    /// </summary>
    [MetadataType(typeof(ContactMetadata))]
    public abstract class ContactModel : EntityModel
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public virtual string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if this contact is the default contact.
        /// </summary>
        public virtual bool Default { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public virtual string Value { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public virtual int PersonId { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if we can add a new contact for active person.
        /// </summary>
        public virtual bool CanAdd { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if we can remove a new contact for active person.
        /// </summary>
        public virtual bool CanRemove { get; set; }
    }
}