using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Coach Entity.
    /// </summary>
    [Table("Coachs")]
    [MetadataType(typeof(CoachMetadata))]
    public class Coach : Person
    {
    }
}