using System.Collections.Generic;
using System.Runtime.Serialization;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos.Person
{
    /// <summary>
    /// Data Transfer Object for Player item.
    /// </summary>
    [DataContract]
    public class PlayerDto : PersonDto
    {
        [DataMember]
        public int? CategoryId { get; set; }

        [DataMember]
        public Laterality Laterality { get; set; }

        [DataMember]
        public int? Height { get; set; }

        [DataMember]
        public int? Weight { get; set; }

        [DataMember]
        public int? ShoesSize { get; set; }

        [DataMember]
        public ICollection<PlayerPositionDto> Positions { get; set; }
    }
}