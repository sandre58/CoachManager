using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Stadium Entity.
    /// </summary>
    public class StadiumMetadata : EntityMetadata
    {
        [Display(Name = "Name", ResourceType = typeof(StadiumResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Name { get; set; }

        [Display(Name = "Ground", ResourceType = typeof(StadiumResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(Ground.Turf)]
        public Ground Ground { get; set; }

        [Display(Name = "Address", ResourceType = typeof(StadiumResources))]
        public int? AddressId { get; set; }
    }
}