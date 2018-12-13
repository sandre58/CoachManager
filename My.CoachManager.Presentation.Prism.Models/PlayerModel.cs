using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.Presentation.Prism.Models
{
    public class PlayerModel : PersonModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PlayerModel"/>.
        /// </summary>
        public PlayerModel()
        {
            Laterality = PlayerConstants.DefaultLaterality;
            Positions = new ObservableCollection<PlayerPositionModel>();
        }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [Display(Name = "Category", ResourceType = typeof(PlayerResources))]
        public CategoryModel Category { get; set; }

        /// <summary>
        /// Gets or sets the latérality.
        /// </summary>
        [Display(Name = "Laterality", ResourceType = typeof(PlayerResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public Laterality Laterality { get; set; }

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        [Display(Name = "Height", ResourceType = typeof(PlayerResources))]
        public int? Height { get; set; }

        /// <summary>
        /// Gets or sets the weight.
        /// </summary>
        [Display(Name = "Weight", ResourceType = typeof(PlayerResources))]
        public int? Weight { get; set; }

        /// <summary>
        /// Gets or sets the shoes size.
        /// </summary>
        [Display(Name = "ShoesSize", ResourceType = typeof(PlayerResources))]
        public int? ShoesSize { get; set; }

        /// <summary>
        /// Gets or set the positions.
        /// </summary>
        [Display(Name = "Positions", ResourceType = typeof(PlayerResources))]
        public ObservableCollection<PlayerPositionModel> Positions { get; set; }
    }
}