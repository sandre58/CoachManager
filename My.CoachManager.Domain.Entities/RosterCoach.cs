using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Roster Coach Entity.
    /// </summary>
    [MetadataType(typeof(RosterCoachMetadata))]
    public class RosterCoach : Entity
    {
        /// <summary>
        /// Gets or sets the coach's roster id.
        /// </summary>
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the coach's roster.
        /// </summary>
        public Roster Roster { get; set; }

        /// <summary>
        /// Gets or sets the coach id.
        /// </summary>
        public int CoachId { get; set; }

        /// <summary>
        /// Gets or sets the coach.
        /// </summary>
        public Coach Coach { get; set; }

        /// <summary>
        /// Gets or sets the function id.
        /// </summary>
        public int FunctionId { get; set; }

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        public Function Function { get; set; }
    }
}