using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using System.ComponentModel.DataAnnotations.Schema;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Player Entity.
    /// </summary>
    [Table("Players")]
    [MetadataType(typeof(PlayerMetadata))]
    public class Player : Person
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Player"/>.
        /// </summary>
        public Player()
        {
            Laterality = PlayerConstants.DefaultLaterality;
        }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the latérality.
        /// </summary>
        public Laterality Laterality { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets the shoes size.
        /// </summary>
        public int? ShoesSize { get; set; }
    }
}