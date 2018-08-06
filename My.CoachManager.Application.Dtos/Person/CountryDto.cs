using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Person
{
    /// <summary>
    /// Data Transfer Object for Country item.
    /// </summary>
    [DataContract]
    public class CountryDto : ReferenceDto
    {
        [DataMember]
        public string Flag { get; set; }
    }
}