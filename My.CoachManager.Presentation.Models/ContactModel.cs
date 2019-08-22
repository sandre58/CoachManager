using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Models;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Contact item.
    /// </summary>
    public abstract class ContactModel : EntityModel
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        [Display(Name = "Label", ResourceType = typeof(ContactResources))]
        public virtual string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if this contact is the default contact.
        /// </summary>
        [Display(Name = "Default", ResourceType = typeof(ContactResources))]
        public virtual bool Default { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public virtual string Value { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        [Display(Name = "Person", ResourceType = typeof(ContactResources))]
        public virtual int? PersonId { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether the specified object is equal to the current object.
        /// </summary>
        /// <param name="obj">The object to compare with the current object.</param>
        /// <returns>true if the specified object is equal to the current object; otherwise, false.</returns>
        public override bool Equals(object obj)
        {
            var other = obj as ContactModel;
            return !(other is null) && base.Equals(other) && string.Equals(Label, other.Label) && Default == other.Default && string.Equals(Value, other.Value) && PersonId == other.PersonId;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}