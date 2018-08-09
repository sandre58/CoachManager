using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Season Entity.
    /// </summary>
    public class Season : Reference
    {
        /// <summary>
        /// Gets or sets the start date.
        /// </summary>
        [Required]
        public DateTime? StartDate { get; set; }

        /// <summary>
        /// Gets or sets the end date.
        /// </summary>
        [Required]
        public DateTime? EndDate { get; set; }
    }
}