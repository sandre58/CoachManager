using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    [MetadataType(typeof(RosterMetadata))]
    public class RosterModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="RosterModel"/>.
        /// </summary>
        public RosterModel()
        {
            Players = new ObservableCollection<RosterPlayerModel>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the season id.
        /// </summary>
        public int? SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        public SeasonModel Season { get; set; }

        /// <summary>
        /// Gets or sets the category id.
        /// </summary>
        public int? CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the category.
        /// </summary>
        public CategoryModel Category { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ObservableCollection<RosterPlayerModel> Players { get; set; }
    }
}