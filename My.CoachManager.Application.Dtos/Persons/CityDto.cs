using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Players list Dtos.
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