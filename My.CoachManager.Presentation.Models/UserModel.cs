using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a User Entity.
    /// </summary>
    public class User : EntityModel
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Display(Name = "Name", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(150, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        [Display(Name = "Login", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(100, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Display(Name = "Password", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        [Display(Name = "Mail", ResourceType = typeof(UserResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [MaxLength(200, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [EmailAddress(ErrorMessageResourceName = "EmailFormatMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Mail { get; set; }
    }
}
