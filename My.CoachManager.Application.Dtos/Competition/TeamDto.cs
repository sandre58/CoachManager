using System.Runtime.Serialization;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Application.Dtos.Competition
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