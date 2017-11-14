using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Users
{
    [DataContract]
    public class PermissionDto : EntityDto
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Description { get; set; }
    }
}