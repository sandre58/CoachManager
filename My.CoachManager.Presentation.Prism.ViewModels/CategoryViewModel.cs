using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(CategoryMetadata))]
    public class CategoryViewModel : DataEntityViewModel
    {
        private int? _year;
        public virtual int? Year { get { return _year; } set { SetProperty(ref _year, value); } }
    }
}