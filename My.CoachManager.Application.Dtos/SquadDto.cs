using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Provides properties for a Squad Entity.
    /// </summary>
    public class SquadDto : EntityDto
    {

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the squad's roster id.
        /// </summary>
        [DataMember]
        public int RosterId { get; set; }

    }
}