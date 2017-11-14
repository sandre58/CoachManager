using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Attributes;

namespace My.CoachManager.Presentation.ViewModels.Metadatas
{
    public class EmailMetadata : ContactMetadata
    {
        [Display(Name = "Phone", ResourceType = typeof(ContactResources))]
        [Email(ErrorMessageResourceName = "EmailFormatMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Value { get; set; }
    }
}