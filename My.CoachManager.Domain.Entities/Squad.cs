using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Squad Entity.
    /// </summary>
    [MetadataType(typeof(SquadMetadata))]
    public class Squad : DataEntity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Squad"/>.
        /// </summary>
        protected Squad()
        {
            Players = new List<RosterPlayer>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ICollection<RosterPlayer> Players { get; set; }
    }
}