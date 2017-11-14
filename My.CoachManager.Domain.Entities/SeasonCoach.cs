using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(SeasonCoachMetadata))]
    public class SeasonCoach : ForeignEntity
    {
        public int SeasonId { get; set; }

        public Season Season { get; set; }

        public int CoachId { get; set; }

        public Coach Coach { get; set; }
    }
}