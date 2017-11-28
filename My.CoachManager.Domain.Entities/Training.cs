using System;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Training Entity.
    /// </summary>
    public class Training : Entity
    {
        /// <summary>
        /// Gets or sets the date and time.
        /// </summary>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the duration (in minutes).
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets the stadium id.
        /// </summary>
        public int StadiumId { get; set; }

        /// <summary>
        /// Gets or sets the stadium.
        /// </summary>
        public Stadium Stadium { get; set; }
    }
}