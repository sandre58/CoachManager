using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    /// <summary>
    /// Provides properties for a Position model.
    /// </summary>
    public class PositionModel : ReferenceModel
    {
        /// <summary>
        /// Initalize a new instance of <see cref="PositionModel"/>.
        /// </summary>
        public PositionModel()
        {
            Side = PositionConstants.DefaultSide;
            Type = PositionConstants.DefaultType;
        }

        /// <summary>
        /// Gets or sets the path of the postion side.
        /// </summary>
        [Display(Name = "Side", ResourceType = typeof(PositionResources))]
        public PositionSide Side { get; set; }

        /// <summary>
        /// Gets or sets the path of the postion type.
        /// </summary>
        [Display(Name = "Type", ResourceType = typeof(PositionResources))]
        public PositionType Type { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        [Display(Name = "Row", ResourceType = typeof(PositionResources))]
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        [Display(Name = "Column", ResourceType = typeof(PositionResources))]
        public int Column { get; set; }
    }
}