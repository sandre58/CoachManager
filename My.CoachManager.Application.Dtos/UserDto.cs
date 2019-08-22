using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
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
    }
}
