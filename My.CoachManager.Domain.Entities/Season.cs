using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(SeasonMetadata))]
    public class Season : DataEntity
    {
        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
    }
}