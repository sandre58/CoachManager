using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Country Entity.
    /// </summary>
    [MetadataType(typeof(CountryMetadata))]
    public class Country : Reference
    {
        /// <summary>
        /// Gets or sets the path of the flag image.
        /// </summary>
        public string Flag { get; set; }
    }
}