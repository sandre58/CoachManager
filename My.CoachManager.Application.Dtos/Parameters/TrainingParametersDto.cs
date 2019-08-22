using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Parameters
{
    /// <summary>
    /// Data Transfer Object for Address item.
    /// </summary>
    [DataContract]
    public class TrainingParametersDto
    {
        /// <summary>
        /// Gets or set parameter.
        /// </summary>
        [DataMember]
        public int TrainingId { get; set; }

        /// <summary>
        /// Gets or set parameter.
        /// </summary>
        [DataMember]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or set parameter.
        /// </summary>
        [DataMember]
        public string Place { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public IEnumerable<TrainingAttendanceDto> Attendances { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public TimeSpan StartTime { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public TimeSpan EndTime { get; set; }

        /// <summary>
        /// Gets or set parameter.
        /// </summary>
        [DataMember]
        public IList<DayOfWeek> Days { get; set; }
    }
}