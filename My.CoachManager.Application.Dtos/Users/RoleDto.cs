using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Users
{
    [DataContract]
    public class RoleDto : ReferenceDto
    {
        [DataMember]
        public ICollection<PermissionDto> Permissions { get; set; }
    }
}