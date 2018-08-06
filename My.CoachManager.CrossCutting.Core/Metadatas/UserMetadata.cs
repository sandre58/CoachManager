using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a User Entity.
    /// </summary>
    public class UserMetadata : EntityMetadata
    {
        [Display(Name = "Name", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(150, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Index]
        public string Name { get; set; }

        [Display(Name = "Login", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Index]
        public string Login { get; set; }

        [Display(Name = "Password", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Password { get; set; }

        [Display(Name = "Mail", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Index]
        [EmailAddress(ErrorMessageResourceName = "EmailFormatMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Mail { get; set; }

        [Display(Name = "Roles", ResourceType = typeof(UserResources))]
        public object Roles { get; set; }
    }
}