using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Team Entity.
    /// </summary>
    [MetadataType(typeof(TeamMetadata))]
    public class Team : Entity
    {
        /// <summary>
        /// Gets or sets the team's club id.
        /// </summary>
        public int ClubId { get; set; }

        /// <summary>
        /// Gets or sets the team's club.
        /// </summary>
        public Club Club { get; set; }

        /// <summary>
        /// Gets or sets the team's category id.
        /// </summary>
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the team's category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }
    }
}