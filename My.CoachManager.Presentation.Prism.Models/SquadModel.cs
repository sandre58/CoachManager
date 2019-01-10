using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
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

        /// <summary>
        /// Gets or sets the squad's roster id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Roster", ResourceType = typeof(SquadResources))]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the squad's roster.
        /// </summary>
        public RosterModel Roster { get; set; }
    }
}