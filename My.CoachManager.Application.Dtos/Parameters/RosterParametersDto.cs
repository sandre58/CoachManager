using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Parameters
{
    /// <summary>
    /// Data Transfer Object for Address item.
    /// </summary>
    [DataContract]
    public class RosterParametersDto
    {
        /// <summary>
        /// Gets or set rosterId.
        /// </summary>
        [DataMember]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public int SquadId { get; set; }

        /// <summary>
        /// Gets or set a parameter.
        /// </summary>
        [DataMember]
        public IEnumerable<int> PlayersId { get; set; }
    }
}