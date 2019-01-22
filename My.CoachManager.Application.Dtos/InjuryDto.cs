using System;
using System.Runtime.Serialization;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Provides properties for a injury Entity.
    /// </summary>
    public class InjuryDto : EntityDto
    {

        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        [DataMember]
        public virtual string Condition { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [DataMember]
        public int? PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [DataMember]
        public virtual InjuryType Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [DataMember]
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        [DataMember]
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the expected return date.
        /// </summary>
        [DataMember]
        public virtual DateTime? ExpectedReturn { get; set; }
    }
}