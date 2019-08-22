using System;
using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a injury Entity.
    /// </summary>
    [Serializable]
    public class Injury : Entity
    {
        /// <summary>
        /// Gets or sets the condition.
        /// </summary>
        [Required]
        public virtual string Condition { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [Required]
        public int? PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public virtual Player Player { get; set; }

        /// <summary>
        /// Gets or sets the type.
        /// </summary>
        [Required]
        public virtual InjuryType Type { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public virtual string Description { get; set; }

        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the expected return date.
        /// </summary>
        public virtual DateTime? ExpectedReturn { get; set; }

        /// <summary>
        /// Gets or sets the severity.
        /// </summary>
        public virtual InjurySeverity Severity { get; set; }
    }
}
