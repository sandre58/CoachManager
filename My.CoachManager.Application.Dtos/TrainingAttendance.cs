using System.Runtime.Serialization;

using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Provides properties for a training attendance Entity.
    /// </summary>
    public class TrainingAttendanceDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [DataMember]
        public int RosterPlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        [DataMember]
        public RosterPlayerDto RosterPlayer { get; set; }

        /// <summary>
        /// Gets or sets the attendance.
        /// </summary>
        [DataMember]
        public Attendance Attendance { get; set; }

        /// <summary>
        /// Gets or sets the reason.
        /// </summary>
        [DataMember]
        public string Reason { get; set; }
    }
}
