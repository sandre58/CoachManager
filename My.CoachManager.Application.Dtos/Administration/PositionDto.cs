using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Administration
{
    /// <summary>
    /// Data Transfer Object for Position item.
    /// </summary>
    [DataContract]
    public class PositionDto : DataEntityDto
    {
        [DataMember]
        public int Row { get; set; }

        [DataMember]
        public int Column { get; set; }
    }
}