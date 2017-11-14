using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PositionMetadata))]
    public class PositionViewModel : DataEntityViewModel
    {
        private int _row;
        public virtual int Row { get { return _row; } set { SetProperty(ref _row, value); } }

        private int _column;
        public virtual int Column { get { return _column; } set { SetProperty(ref _column, value); } }
    }
}