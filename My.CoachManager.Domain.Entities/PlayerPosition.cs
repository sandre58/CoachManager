using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Player Position Entity.
    /// </summary>
    [MetadataType(typeof(PlayerPositionMetadata))]
    public class PlayerPosition : Entity
    {
        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the position id.
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        public decimal Rating { get; set; }
    }
}