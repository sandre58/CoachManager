using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class PhoneMetadata : ContactMetadata
    {
        [Display(Name = "Phone", ResourceType = typeof(ContactResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(10, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public override string Value { get; set; }
    }
}