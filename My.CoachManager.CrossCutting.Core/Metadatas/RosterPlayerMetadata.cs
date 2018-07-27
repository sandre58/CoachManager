using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Roster Player Entity.
    /// </summary>
    public class RosterPlayerMetadata : EntityMetadata
    {
        [Key]
        [Column(Order = 1)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int RosterId { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PlayerId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Squad", ResourceType = typeof(PlayerResources))]
        public int SquadId { get; set; }

        [Display(Name = "Number", ResourceType = typeof(PlayerResources))]
        [Range(1, 99, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int? Number { get; set; }

        [Display(Name = "LicenseState", ResourceType = typeof(PlayerResources))]
        [DefaultValue(LicenseState.Unknown)]
        public LicenseState LicenseState { get; set; }

        [Display(Name = "IsMutation", ResourceType = typeof(PlayerResources))]
        public bool IsMutation { get; set; }
    }
}