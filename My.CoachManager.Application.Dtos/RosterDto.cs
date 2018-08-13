using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Roster item.
    /// </summary>
    [DataContract]
    public class RosterDto : EntityDto
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the season id.
        /// </summary>
        [DataMember]
        public int? SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        [DataMember]
        public virtual SeasonDto Season { get; set; }

        /// <summary>
        /// Gets or sets the squad's category id.
        /// </summary>
        [DataMember]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the squad's category.
        /// </summary>
        [DataMember]
        public virtual CategoryDto Category { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        [DataMember]
        public IEnumerable<RosterPlayerDto> Players { get; set; }
        
    }
}