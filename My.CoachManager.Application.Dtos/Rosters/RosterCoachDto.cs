using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Administration;
using My.CoachManager.Application.Dtos.Persons;

namespace My.CoachManager.Application.Dtos.Rosters
{
    /// <summary>
    /// Data Transfer Object for Roster coach item.
    /// </summary>
    [DataContract]
    public class RosterCoachDto : EntityDtoBase
    {
        /// <summary>
        /// Gets or sets the coach's roster id.
        /// </summary>
        [DataMember]
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the coach's roster.
        /// </summary>
        [DataMember]
        public RosterDto Roster { get; set; }

        /// <summary>
        /// Gets or sets the coach id.
        /// </summary>
        [DataMember]
        public int CoachId { get; set; }

        /// <summary>
        /// Gets or sets the coach.
        /// </summary>
        [DataMember]
        public CoachDto Coach { get; set; }

        /// <summary>
        /// Gets or sets the function id.
        /// </summary>
        [DataMember]
        public int FunctionId { get; set; }

        /// <summary>
        /// Gets or sets the function.
        /// </summary>
        [DataMember]
        public FunctionDto Function { get; set; }
    }
}