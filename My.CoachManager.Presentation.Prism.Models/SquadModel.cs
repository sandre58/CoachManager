using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    [MetadataType(typeof(SquadMetadata))]
    public class SquadModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="SquadModel"/>.
        /// </summary>
        public SquadModel()
        {
            Players = new ObservableCollection<SquadPlayerModel>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the roster Id.
        /// </summary>
        public int RosterId { get; set; }

        /// <summary>
        /// Gets or sets the roster.
        /// </summary>
        public RosterModel Roster { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ObservableCollection<SquadPlayerModel> Players { get; set; }
    }
}