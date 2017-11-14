using System.Collections.Generic;
using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Admin;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Players list Dtos.
    /// </summary>
    [DataContract]
    public class PlayerDto : PersonDto
    {
        [DataMember]
        public int? CategoryId { get; set; }

        [DataMember]
        public CategoryDto Category { get; set; }

        [DataMember]
        public Laterality Laterality { get; set; }

        [DataMember]
        public int? ShoesSize { get; set; }

        [DataMember]
        public ICollection<PlayerPositionDto> Positions { get; set; }

        [DataMember]
        public ICollection<PlayerHeightDto> Heights { get; set; }

        [DataMember]
        public ICollection<PlayerWeightDto> Weights { get; set; }
    }
}