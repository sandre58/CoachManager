using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Person
{
    /// <summary>
    /// Data Transfer Object for City item.
    /// </summary>
    [DataContract]
    public class CityDto
    {
        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }
    }
}