using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Admin
{
    /// <summary>
    /// Players list Dtos.
    /// </summary>
    [DataContract]
    public class CategoryDto : DataEntityDto
    {
        [DataMember]
        public int? Year { get; set; }
    }
}