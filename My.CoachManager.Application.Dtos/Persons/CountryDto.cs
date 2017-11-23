using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Data Transfer Object for Country item.
    /// </summary>
    [DataContract]
    public class CountryDto : DataEntityDto
    {
        [DataMember]
        public string Flag { get; set; }
    }
}