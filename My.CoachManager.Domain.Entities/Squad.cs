using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Squad Entity.
    /// </summary>
    [MetadataType(typeof(SquadMetadata))]
    public class Squad : Entity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Squad"/>.
        /// </summary>
        public Squad()
        {
            Players = new List<RosterPlayer>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the roster Id.
        /// </summary>
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        public Roster Roster { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ICollection<RosterPlayer> Players { get; set; }
    }
}