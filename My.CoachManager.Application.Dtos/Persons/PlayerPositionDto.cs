using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Administration;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Data Transfer Object for Player Position item.
    /// </summary>
    [DataContract]
    public class PlayerPositionDto : IEntityDtoBase
    {
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
        /// Gets or sets the position id.
        /// </summary>
        [DataMember]
        public int PositionId { get; set; }

        /// <summary>
        /// Gets or sets the position.
        /// </summary>
        [DataMember]
        public PositionDto Position { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        [DataMember]
        public decimal Rating { get; set; }
    }
}