using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Roster Player Entity.
    /// </summary>
    [MetadataType(typeof(RosterPlayerMetadata))]
    public class RosterPlayer : Entity
    {
        /// <summary>
        /// Gets or sets the player's roster id.
        /// </summary>
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the player's roster.
        /// </summary>
        public Roster Roster { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the player's squad id.
        /// </summary>
        public int SquadId { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Gets or sets the license state.
        /// </summary>
        public LicenseState LicenseState { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if the player is in mutation.
        /// </summary>
        public bool IsMutation { get; set; }
    }
}