using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Team Entity.
    /// </summary>
    public class TeamMetadata : EntityMetadata
    {
        [Display(Name = "Name", ResourceType = typeof(TeamResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Name { get; set; }

        [Display(Name = "Club", ResourceType = typeof(TeamResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int ClubId { get; set; }

        [Display(Name = "Category", ResourceType = typeof(TeamResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int CategoryId { get; set; }
    }
}