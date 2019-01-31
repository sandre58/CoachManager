using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Squad Entity.
    /// </summary>
    public class SquadModel : EntityModel
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Name", ResourceType = typeof(SquadResources))]
        public string Name { get; set; }

    }
}