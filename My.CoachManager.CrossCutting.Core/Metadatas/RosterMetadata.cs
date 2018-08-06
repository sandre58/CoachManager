using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides Metadata for a Roster Entity.
    /// </summary>
    public class RosterMetadata : EntityMetadata
    {
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Name", ResourceType = typeof(RosterResources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Season", ResourceType = typeof(RosterResources))]
        public int? SeasonId { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Category", ResourceType = typeof(RosterResources))]
        public int? CategoryId { get; set; }

        [Display(Name = "Players", ResourceType = typeof(RosterResources))]
        public ICollection<object> Players { get; set; }

        [Display(Name = "Squads", ResourceType = typeof(RosterResources))]
        public ICollection<object> Squads { get; set; }
    }
}