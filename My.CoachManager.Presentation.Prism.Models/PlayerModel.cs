using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.Models
{
    [MetadataType(typeof(PlayerMetadata))]
    public class PlayerModel : PersonModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PlayerModel"/>.
        /// </summary>
        public PlayerModel()
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
        public CategoryModel Category { get; set; }

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