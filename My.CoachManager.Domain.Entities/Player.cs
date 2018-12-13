using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Player Entity.
    /// </summary>
    public class Player : Person
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Player"/>.
        /// </summary>
        public Player()
        {
            Laterality = PlayerConstants.DefaultLaterality;
            Positions = new List<PlayerPosition>();
        }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [Required]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public Category Category { get; set; }

        /// <summary>
        /// Gets or sets the latérality.
        /// </summary>
        [Required]
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

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        public ICollection<PlayerPosition> Positions { get; set; }
    }
}