using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos.Rosters
{
    /// <summary>
    /// Data Transfer Object for Roster player item.
    /// </summary>
    [DataContract]
    public class RosterPlayerDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the player's roster id.
        /// </summary>
        [DataMember]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the player's roster.
        /// </summary>
        [DataMember]
        public RosterDto Roster { get; set; }

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
        /// Gets or sets the player's squad id.
        /// </summary>
        [DataMember]
        public int SquadId { get; set; }

        /// <summary>
        /// Gets or sets the player's squad.
        /// </summary>
        [DataMember]
        public SquadDto Squad { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        [DataMember]
        public int? Number { get; set; }

        /// <summary>
        /// Gets or sets the license state.
        /// </summary>
        [DataMember]
        public LicenseState LicenseState { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if the player is in mutation.
        /// </summary>
        [DataMember]
        public bool IsMutation { get; set; }
    }
}