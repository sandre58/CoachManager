using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(CountryMetadata))]
    public class Country : DataEntity
    {
        public string Flag { get; set; }
    }
}