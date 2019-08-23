using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.CrossCutting.Resources.Entities;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a training attendance Entity.
    /// </summary>
    public class TrainingAttendanceModel : EntityModel
    {
        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int RosterPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public RosterPlayerModel Player { get; set; }

        /// <summary>
        /// Gets or sets the attendance.
        /// </summary>
        [Display(Name = "Attendance", ResourceType = typeof(TrainingResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public Attendance Attendance { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [Display(Name = "Reason", ResourceType = typeof(TrainingResources))]
        public string Reason { get; set; }
    }
}
