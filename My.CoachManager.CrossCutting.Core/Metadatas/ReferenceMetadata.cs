using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a entity containing Code, Label, Description and Order.
    /// </summary>
    public class ReferenceMetadata : EntityMetadata
    {
        [Display(Name = "Code", ResourceType = typeof(ReferenceResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(15, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Code { get; set; }

        [Display(Name = "Label", ResourceType = typeof(ReferenceResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Label { get; set; }

        [Display(Name = "Description", ResourceType = typeof(ReferenceResources))]
        public string Description { get; set; }

        [Display(Name = "Order", ResourceType = typeof(ReferenceResources))]
        public int Order { get; set; }
    }
}