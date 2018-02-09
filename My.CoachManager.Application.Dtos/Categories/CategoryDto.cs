using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Administration
{
    /// <summary>
    /// Data Transfer Object for Category item.
    /// </summary>
    [DataContract]
    public class CategoryDto : DataEntityDto
    {
        [DataMember]
        public int? Year { get; set; }
    }
}