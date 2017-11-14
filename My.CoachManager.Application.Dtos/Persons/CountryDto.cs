using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Players list Dtos.
    /// </summary>
    [DataContract]
    public class CountryDto : DataEntityDto
    {
        [DataMember]
        public string Flag { get; set; }
    }
}