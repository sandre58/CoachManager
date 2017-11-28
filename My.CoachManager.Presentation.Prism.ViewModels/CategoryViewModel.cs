using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    [MetadataType(typeof(CategoryMetadata))]
    public class CategoryViewModel : DataEntityViewModel
    {
        private int? _year;

        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        public virtual int? Year { get { return _year; } set { SetProperty(ref _year, value); } }
    }
}