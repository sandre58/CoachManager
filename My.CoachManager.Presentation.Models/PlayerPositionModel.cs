using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Player Position Entity.
    /// </summary>
    public class PlayerPositionModel : EntityModel
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Player"/>.
        /// </summary>
        public PlayerPositionModel()
        {
            Rating = PositionConstants.DefaultRating;
        }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        public PlayerModel Player { get; set; }

        /// <summary>
        /// Gets or sets the player's position id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        public PositionModel Position { get; set; }

        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Range(0, PositionConstants.MaxRating, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int Rating { get; set; }


        /// <summary>
        /// Gets or sets the note.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Range(0, PositionConstants.MaxRating, ErrorMessageResourceName = "RangeFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public PositionRating PositionRating {
            get => (PositionRating)Rating;
            set => Rating = (int)value;
        }

        /// <summary>
        /// Gets or sets a value indicates if the position is natural.
        /// </summary>
        public bool IsNatural { get; set; }
    }
}
