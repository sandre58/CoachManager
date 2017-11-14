using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.Presentation.ViewModels.Metadatas
{
    public class PhoneMetadata : ContactMetadata
    {
        [Display(Name = "Phone", ResourceType = typeof(ContactResources))]
        [MaxLength(10, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [RegularExpression(ContactConstants.PhoneRegex, ErrorMessageResourceName = "PhoneFormatMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Value { get; set; }
    }
}