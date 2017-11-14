using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class SquadMetadata : DataEntityMetadata
    {
        [Display(Name = "Season", ResourceType = typeof(SquadResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int SeasonId { get; set; }
    }
}