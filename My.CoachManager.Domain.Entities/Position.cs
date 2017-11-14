using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(PositionMetadata))]
    public class Position : DataEntity
    {
        public int Row { get; set; }

        public int Column { get; set; }
    }
}