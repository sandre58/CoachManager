using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Category item.
    /// </summary>
    [DataContract]
    public class CategoryDto : ReferenceDto
    {
        [DataMember]
        public int? Age { get; set; }
    }
}