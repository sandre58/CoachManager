using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a foreign entity.
    /// </summary>
    [MetadataType(typeof(ForeignEntityMetadata))]
    public abstract class ForeignEntity : EntityBase
    {
    }
}