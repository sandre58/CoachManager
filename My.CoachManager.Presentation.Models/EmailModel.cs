using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.CrossCutting.Resources.Entities;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Email Entity.
    /// </summary>
    public class EmailModel : ContactModel
    {
        /// <summary>
        /// Provides metadata for a Email Entity.
        /// </summary>
        [Display(Name = "Email", ResourceType = typeof(ContactResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [EmailAddress(ErrorMessageResourceName = "EmailFormatMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public override string Value { get; set; }
    }
}
