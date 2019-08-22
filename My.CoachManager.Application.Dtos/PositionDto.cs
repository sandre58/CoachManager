using System.Runtime.Serialization;

using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Position item.
    /// </summary>
    [DataContract]
    public class PositionDto : ReferenceDto
    {
        [DataMember]
        public PositionSide Side { get; set; }

        [DataMember]
        public PositionType Type { get; set; }

        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Column { get; set; }
    }
}
