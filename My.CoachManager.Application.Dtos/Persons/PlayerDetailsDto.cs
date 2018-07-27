using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Category;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Data Transfer Object for PlayerDetail item.
    /// </summary>
    [DataContract]
    public class PlayerDetailsDto : PlayerDto
    {
        [DataMember]
        public CategoryDto Category { get; set; }

        [DataMember]
        public CountryDto Country { get; set; }
    }
}