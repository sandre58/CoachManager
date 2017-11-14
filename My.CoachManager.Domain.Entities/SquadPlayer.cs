using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(SquadPlayerMetadata))]
    public class SquadPlayer : ForeignEntity
    {
        public int SquadId { get; set; }

        public Squad Squad { get; set; }

        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public bool Captain { get; set; }

        public int? Number { get; set; }
    }
}