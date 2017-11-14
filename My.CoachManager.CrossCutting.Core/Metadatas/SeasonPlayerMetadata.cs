using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class SeasonPlayerMetadata : ForeignEntityMetadata
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int SeasonId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PlayerId { get; set; }

        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int CategoryId { get; set; }

        [Display(Name = "LicenseState", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(PlayerConstants.DefaultLicenseState)]
        public LicenseState LicenseState { get; set; }

        [Display(Name = "Mutation", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(PlayerConstants.DefaultMutation)]
        public bool Mutation { get; set; }

        [Display(Name = "Height", ResourceType = typeof(PlayerResources))]
        public int? Height { get; set; }

        [Display(Name = "Weight", ResourceType = typeof(PlayerResources))]
        public int? Weight { get; set; }
    }
}