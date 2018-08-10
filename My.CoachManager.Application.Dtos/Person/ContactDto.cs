using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Person
{
    /// <summary>
    /// Data Transfer Object for Contact item.
    /// </summary>
    [DataContract]
    [KnownType(typeof(EmailDto))]
    [KnownType(typeof(PhoneDto))]
    public class ContactDto : EntityDto
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public bool Default { get; set; }

        [DataMember]
        public int? PersonId { get; set; }
    }
}