using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Position Entity.
    /// </summary>
    public class Position : Reference
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Position"/>.
        /// </summary>
        public Position()
        {
            Side = PositionConstants.DefaultSide;
            Type = PositionConstants.DefaultType;
        }

        /// <summary>
        /// Gets or sets the path of the postion side.
        /// </summary>
        [Required]
        public PositionSide Side { get; set; }

        /// <summary>
        /// Gets or sets the path of the postion type.
        /// </summary>
        [Required]
        public PositionType Type { get; set; }

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        [Required]
        public int Row { get; set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        [Required]
        public int Column { get; set; }
    }
}