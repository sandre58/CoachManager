using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos.Roster
{
    /// <summary>
    /// Data Transfer Object for Player item.
    /// </summary>
    [DataContract]
    public class SquadPlayerDto : PlayerDetailsDto
    {
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