﻿using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Rosters
{
    /// <summary>
    /// Data Transfer Object for Squad item.
    /// </summary>
    [DataContract]
    public class SquadDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the roster Id.
        /// </summary>
        [DataMember]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        [DataMember]
        public RosterDto Roster { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        [DataMember]
        public ICollection<RosterPlayerDto> Players { get; set; }
    }
}