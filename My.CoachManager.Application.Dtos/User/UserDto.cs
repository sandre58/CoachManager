using System.Collections.Generic;
using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.User
{
    [DataContract]
    public class UserDto : EntityDto
    {
        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Login { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public string Mail { get; set; }

        [DataMember]
        public ICollection<RoleDto> Roles { get; set; }
    }
}