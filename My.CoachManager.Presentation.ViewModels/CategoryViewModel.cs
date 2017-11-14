using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(CategoryMetadata))]
    public class CategoryViewModel : DataEntityViewModel
    {
        private int? _year;
        public virtual int? Year { get { return _year; } set { SetEntityProperty(() => _year = value, value, Year); } }
    }
}