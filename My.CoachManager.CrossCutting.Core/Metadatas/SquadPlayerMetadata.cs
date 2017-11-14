using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class SquadPlayerMetadata : ForeignEntityMetadata
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int SquadId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PlayerId { get; set; }

        [Display(Name = "Captain", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [DefaultValue(false)]
        public bool Captain { get; set; }

        [Display(Name = "Number", ResourceType = typeof(PlayerResources))]
        [Range(PlayerConstants.MinNumber, PlayerConstants.MaxNumber, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int? Number { get; set; }
    }
}