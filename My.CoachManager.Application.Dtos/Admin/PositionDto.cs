using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Admin
{
    /// <summary>
    /// Players list Dtos.
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