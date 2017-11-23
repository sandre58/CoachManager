using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Contact Entity.
    /// </summary>
    public abstract class ContactMetadata : EntityMetadata
    {
        [Display(Name = "Label", ResourceType = typeof(ContactResources))]
        public string Label { get; set; }

        [Display(Name = "Default", ResourceType = typeof(ContactResources))]
        public bool Default { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public virtual string Value { get; set; }

        [Display(Name = "Person", ResourceType = typeof(ContactResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PersonId { get; set; }
    }
}