using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Reference Dto.
    /// </summary>
    [DataContract]
    public class ReferenceDto : EntityDto
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public int Order { get; set; }
    }
}