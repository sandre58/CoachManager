using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Category item.
    /// </summary>
    [MetadataType(typeof(CategoryMetadata))]
    public class CategoryModel : ReferenceModel
    {
        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        public virtual int? Year { get; set; }
    }
}