using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Category Entity.
    /// </summary>
    [MetadataType(typeof(CategoryMetadata))]
    public class Category : DataEntity
    {
        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        public int? Year { get; set; }
    }
}