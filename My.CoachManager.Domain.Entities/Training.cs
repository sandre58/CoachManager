using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a training Entity.
    /// </summary>
    public class Training : Entity
    {

        /// <summary>
        /// Gets or sets the roster id.
        /// </summary>
        [Required]
        public int? RosterId { get; set; }

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        public virtual Roster Roster { get; set; }

        /// <summary>
        /// Gets or sets the place.
        /// </summary>
        public virtual string Place { get; set; }

        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required]
        public virtual DateTime StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Required]
        public virtual DateTime EndDate { get; set; }

        /// <summary>
        /// Gets or sets the is cancelled value.
        /// </summary>
        public virtual bool IsCancelled { get; set; }
    }
}