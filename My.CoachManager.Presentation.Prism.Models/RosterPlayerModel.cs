using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    public class RosterPlayerModel : PlayerModel
    {
        /// <summary>
        /// Gets or sets the player's roster id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the player's roster.
        /// </summary>
        public RosterModel Roster { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        [Display(Name = "Number", ResourceType = typeof(PlayerResources))]
        [Range(1, 99, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int? Number { get; set; }

        /// <summary>
        /// Gets or sets the license state.
        /// </summary>
        [Display(Name = "LicenseState", ResourceType = typeof(PlayerResources))]
        public LicenseState LicenseState { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if the player is in mutation.
        /// </summary>
        [Display(Name = "IsMutation", ResourceType = typeof(PlayerResources))]
        public bool IsMutation { get; set; }
    }
}