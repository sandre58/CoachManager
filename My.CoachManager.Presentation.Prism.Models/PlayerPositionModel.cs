using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    [MetadataType(typeof(PlayerPositionMetadata))]
    public class PlayerPositionModel : EntityModel
    {
        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        public int? PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public PlayerModel Player { get; set; }

        /// <summary>
        /// Gets or sets the position id.
        /// </summary>
        public int? PositionId { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        public PositionModel Position { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        public int Rating { get; set; }
    }
}