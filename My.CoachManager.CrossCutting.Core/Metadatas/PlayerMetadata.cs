using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class PlayerMetadata : PersonMetadata
    {
        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int CategoryId { get; set; }

        [Display(Name = "Laterality", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(PlayerConstants.DefaultLaterality)]
        public Laterality Laterality { get; set; }

        [Display(Name = "ShoesSize", ResourceType = typeof(PlayerResources))]
        public int? ShoesSize { get; set; }

        [Display(Name = "Positions", ResourceType = typeof(PlayerResources))]
        public object Positions { get; set; }

        [Display(Name = "Heights", ResourceType = typeof(PlayerResources))]
        public object Heights { get; set; }

        [Display(Name = "Weights", ResourceType = typeof(PlayerResources))]
        public object Weights { get; set; }
    }
}