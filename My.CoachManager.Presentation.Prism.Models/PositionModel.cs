using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Position Entity.
    /// </summary>
    [MetadataType(typeof(PositionMetadata))]
    public class PositionModel : ReferenceModel
    {

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        public virtual int Row { get; set; }

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        public virtual int Column { get; set; }
    }
}