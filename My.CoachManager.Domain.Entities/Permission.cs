using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Permission Entity.
    /// </summary>
    [MetadataType(typeof(PermissionMetadata))]
    public class Permission : DataEntity
    {
    }
}