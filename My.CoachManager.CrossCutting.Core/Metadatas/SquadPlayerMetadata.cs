using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Roster Player Entity.
    /// </summary>
    public class SquadPlayerMetadata : PlayerMetadata
    {
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