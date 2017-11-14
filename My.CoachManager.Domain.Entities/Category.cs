using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(CategoryMetadata))]
    public class Category : DataEntity
    {
        public int? Year { get; set; }
    }
}