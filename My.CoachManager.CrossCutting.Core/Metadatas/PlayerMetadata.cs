using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Player Entity.
    /// </summary>
    public class PlayerMetadata : PersonMetadata
    {
        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int? CategoryId { get; set; }

        [Display(Name = "Laterality", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public Laterality Laterality { get; set; }

        [Display(Name = "Height", ResourceType = typeof(PlayerResources))]
        public int? Height { get; set; }

        [Display(Name = "Weight", ResourceType = typeof(PlayerResources))]
        public int? Weight { get; set; }

        [Display(Name = "ShoesSize", ResourceType = typeof(PlayerResources))]
        public int? ShoesSize { get; set; }
    }
}