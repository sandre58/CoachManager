using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Roster player item.
    /// </summary>
    [DataContract]
    public class PlayerPositionDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the player's position id.
        /// </summary>
        [DataMember]
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the player's position.
        /// </summary>
        [DataMember]
        public PositionDto Position { get; set; }

        /// <summary>
        /// Gets or sets the player id.
        /// </summary>
        [DataMember]
        public int PlayerId { get; set; }

        /// <summary>
        /// Gets or sets the player.
        /// </summary>
        [DataMember]
        public PlayerDto Player { get; set; }

        /// <summary>
        /// Gets or sets the default player's note.
        /// </summary>
        [DataMember]
        public int Rating { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if the position is natural.
        /// </summary>
        [DataMember]
        public bool IsNatural { get; set; }
    }
}
