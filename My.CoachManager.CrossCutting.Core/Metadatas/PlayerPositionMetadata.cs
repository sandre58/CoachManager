using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class PlayerPositionMetadata : ForeignEntityMetadata
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PlayerId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PositionId { get; set; }

        [Display(Name = "Natural", ResourceType = typeof(PositionResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(false)]
        public bool Natural { get; set; }

        [Display(Name = "Rating", ResourceType = typeof(PositionResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(5)]
        [Range(PositionConstants.MinRating, PositionConstants.MaxRating, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Rating { get; set; }
    }
}