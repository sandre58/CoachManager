using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Club Entity.
    /// </summary>
    [MetadataType(typeof(ClubMetadata))]
    public class Club : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Gets or sets the small name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the abbreviation.
        /// </summary>
        public string Abbreviation { get; set; }

        /// <summary>
        /// Gets or sets the logo.
        /// </summary>
        public byte[] Logo { get; set; }

        /// <summary>
        /// Gets or sets the home color.
        /// </summary>
        public string HomeColor { get; set; }

        /// <summary>
        /// Gets or sets the away color.
        /// </summary>
        public string AwayColor { get; set; }

        /// <summary>
        /// Gets or set the teams.
        /// </summary>
        public ICollection<Team> Teams { get; set; }
    }
}