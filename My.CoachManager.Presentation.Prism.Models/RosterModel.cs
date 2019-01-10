using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    public class RosterModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="RosterModel"/>.
        /// </summary>
        public RosterModel()
        {
            Players = new ObservableCollection<RosterPlayerModel>();
            Squads = new ObservableCollection<SquadModel>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Name", ResourceType = typeof(RosterResources))]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the season id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Season", ResourceType = typeof(RosterResources))]
        public int? SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        [Display(Name = "Season", ResourceType = typeof(RosterResources))]
        public SeasonModel Season { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [Display(Name = "Category", ResourceType = typeof(RosterResources))]
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        [Display(Name = "Category", ResourceType = typeof(RosterResources))]
        public CategoryModel Category { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        [Display(Name = "Players", ResourceType = typeof(RosterResources))]
        public ObservableCollection<RosterPlayerModel> Players { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        [Display(Name = "Squads", ResourceType = typeof(RosterResources))]
        public ObservableCollection<SquadModel> Squads { get; set; }

        /// <summary>
        /// Gets or set the mainSquad.
        /// </summary>
        public SquadModel MainSquad => Squads?.FirstOrDefault();
    }
}