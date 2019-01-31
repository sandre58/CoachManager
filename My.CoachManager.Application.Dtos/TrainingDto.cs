using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class TrainingDto : EntityDto
    {

        /// <summary>
        /// Gets or sets the roster id.
        /// </summary>
        [DataMember]
        public int? RosterId { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        [DataMember]
        public virtual string Place { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [DataMember]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [DataMember]
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the is cancelled value.
        /// </summary>
        [DataMember]
        public virtual bool IsCancelled { get; set; }

        /// <summary>
        /// Gets or sets the training attendances.
        /// </summary>
        public IEnumerable<TrainingAttendanceDto> Attendances { get; set; }
    }
}