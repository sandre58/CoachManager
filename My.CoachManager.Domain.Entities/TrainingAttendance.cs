using System;
using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a training attendance Entity.
    /// </summary>
    [Serializable]
    public class TrainingAttendance : Entity
    {
        /// <summary>
        /// Gets or sets the player's training id.
        /// </summary>
        [Required]
        public int TrainingId { get; set; }

        /// <summary>
        /// Gets or sets the player's training.
        /// </summary>
        public Training Training { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [Required]
        public int RosterPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public RosterPlayer RosterPlayer { get; set; }

        /// <summary>
        /// Gets or sets the attendance.
        /// </summary>
        public Attendance Attendance { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        public string Reason { get; set; }
    }
}
