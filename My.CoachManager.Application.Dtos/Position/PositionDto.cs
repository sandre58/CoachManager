using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Position
{
    /// <summary>
    /// Data Transfer Object for Position item.
    /// </summary>
    [DataContract]
    public class PositionDto : ReferenceDto
    {
        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Column { get; set; }
    }
}