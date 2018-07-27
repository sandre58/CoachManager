using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Contact Entity.
    /// </summary>
    [MetadataType(typeof(ContactMetadata))]
    public abstract class Contact : Entity
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if this contact is the default contact.
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the Business identifier.
        /// </summary>
        public override string BusinessKey
        {
            get { return Value; }
        }
    }
}