using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(SquadMetadata))]
    public class Squad : DataEntity
    {
        public int SeasonId { get; set; }

        public Season Season { get; set; }
    }
}