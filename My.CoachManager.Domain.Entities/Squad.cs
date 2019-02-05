using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Squad Entity.
    /// </summary>
    [Serializable]
    public class Squad : Entity
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the squad's roster id.
        /// </summary>
        [Required]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the squad's roster.
        /// </summary>
        public Roster Roster { get; set; }

    }
}