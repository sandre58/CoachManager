using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Country Entity.
    /// </summary>
    [MetadataType(typeof(CountryMetadata))]
    public class Country : DataEntity
    {
        /// <summary>
        /// Gets or sets the path of the flag image.
        /// </summary>
        public string Flag { get; set; }
    }
}