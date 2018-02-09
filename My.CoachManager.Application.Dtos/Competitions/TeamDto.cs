using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Categories;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Application.Dtos.Competitions
{
    /// <summary>
    /// Provides properties for a Team Entity.
    /// </summary>
    [DataContract]
    public class TeamDto : Entity
    {
        /// <summary>
        /// Gets or sets the team's category id.
        /// </summary>
        [DataMember]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the team's category.
        /// </summary>
        [DataMember]
        public CategoryDto Category { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [DataMember]
        public string Name { get; set; }
    }
}