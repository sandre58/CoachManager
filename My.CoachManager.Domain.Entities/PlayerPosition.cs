using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Player Position Entity.
    /// </summary>
    public class PlayerPosition : Entity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Player"/>.
        /// </summary>
        public PlayerPosition()
        {
            Rating = PositionConstants.DefaultRating;
        }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [Required]
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public Player Player { get; set; }

        /// <summary>
        /// Gets or sets the player's position id.
        /// </summary>
        [Required]
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public Position Position { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        [Range(0, PositionConstants.MaxRating, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if the position is natural.
        /// </summary>
        public bool IsNatural { get; set; }
    }
}