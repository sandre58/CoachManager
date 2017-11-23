using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides Metadata for a Club Entity.
    /// </summary>
    public class ClubMetadata : EntityMetadata
    {
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "FullName", ResourceType = typeof(ClubResources))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Name", ResourceType = typeof(ClubResources))]
        public string Name { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Abbreviation", ResourceType = typeof(ClubResources))]
        public string Abbreviation { get; set; }

        [Display(Name = "Logo", ResourceType = typeof(ClubResources))]
        public byte[] Logo { get; set; }

        [Display(Name = "HomeColor", ResourceType = typeof(ClubResources))]
        public string HomeColor { get; set; }

        [Display(Name = "AwayColor", ResourceType = typeof(ClubResources))]
        public string AwayColor { get; set; }

        [Display(Name = "Teams", ResourceType = typeof(ClubResources))]
        public ICollection<object> Teams { get; set; }
    }
}