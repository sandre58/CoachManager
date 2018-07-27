using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Permission Entity.
    /// </summary>
    [MetadataType(typeof(PermissionMetadata))]
    public class Permission : Reference
    {
    }
}