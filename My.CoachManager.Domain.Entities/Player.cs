using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace My.CoachManager.Domain.Entities
{
    [Table("Players")]
    [MetadataType(typeof(PlayerMetadata))]
    public class Player : Person
    {
        public Player()
        {
            Laterality = PlayerConstants.DefaultLaterality;
            Positions = new HashSet<PlayerPosition>();
            Heights = new HashSet<PlayerHeight>();
            Weights = new HashSet<PlayerWeight>();
        }

        public int CategoryId { get; set; }

        public Category Category { get; set; }

        public Laterality Laterality { get; set; }

        public int? ShoesSize { get; set; }

        public ICollection<PlayerPosition> Positions { get; set; }

        public ICollection<PlayerHeight> Heights { get; set; }

        public ICollection<PlayerWeight> Weights { get; set; }
    }
}