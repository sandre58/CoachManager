using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Stadium Entity.
    /// </summary>
    [MetadataType(typeof(StadiumMetadata))]
    public class Stadium : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the ground.
        /// </summary>
        public Ground Ground { get; set; }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public Address Address { get; set; }
    }
}