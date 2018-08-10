using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
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

    }
}