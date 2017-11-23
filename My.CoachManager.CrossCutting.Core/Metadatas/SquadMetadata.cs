using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Squad Entity.
    /// </summary>
    public class SquadMetadata : EntityMetadata
    {
        [Display(Name = "Name", ResourceType = typeof(SquadResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Name { get; set; }

        [Display(Name = "Players", ResourceType = typeof(SquadResources))]
        public ICollection<object> Players { get; set; }
    }
}