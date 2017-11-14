using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(PlayerHeightMetadata))]
    public class PlayerHeight : ForeignEntity
    {
        public int PlayerId { get; set; }

        public Player Player { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }
    }
}