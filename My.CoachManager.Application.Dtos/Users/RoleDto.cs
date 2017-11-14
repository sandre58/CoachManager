using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Users
{
    [DataContract]
    public class RoleDto : EntityDto
    {
        [DataMember]
        public string Code { get; set; }

        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public ICollection<PermissionDto> Permissions { get; set; }
    }
}