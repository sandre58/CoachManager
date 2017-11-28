using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Position Entity.
    /// </summary>
    [MetadataType(typeof(PositionMetadata))]
    public class PositionViewModel : DataEntityViewModel
    {
        private int _row;

        /// <summary>
        /// Gets or sets the row.
        /// </summary>
        public virtual int Row { get { return _row; } set { SetProperty(ref _row, value); } }

        private int _column;

        /// <summary>
        /// Gets or sets the column.
        /// </summary>
        public virtual int Column { get { return _column; } set { SetProperty(ref _column, value); } }
    }
}