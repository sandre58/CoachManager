using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Phone Entity.
    /// </summary>
    public class PhoneModel : ContactModel
    {
        [Display(Name = "Phone", ResourceType = typeof(ContactResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(10, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Phone(ErrorMessageResourceName = "PhoneFormatMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public override string Value { get; set; }
    }
}
