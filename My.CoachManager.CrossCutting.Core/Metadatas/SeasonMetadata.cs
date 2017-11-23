using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Season Entity.
    /// </summary>
    public class SeasonMetadata : DataEntityMetadata
    {
        [Display(Name = "StartDate", ResourceType = typeof(SeasonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public DateTime? StartDate { get; set; }

        [Display(Name = "EndDate", ResourceType = typeof(SeasonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public DateTime? EndDate { get; set; }
    }
}