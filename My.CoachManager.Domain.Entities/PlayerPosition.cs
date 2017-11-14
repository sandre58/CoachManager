using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(PlayerPositionMetadata))]
    public class PlayerPosition : ForeignEntity
    {
        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public int PositionId { get; set; }

        public Position Position { get; set; }

        public bool Natural { get; set; }

        public int Rating { get; set; }
    }
}