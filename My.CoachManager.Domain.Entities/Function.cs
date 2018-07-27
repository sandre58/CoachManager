using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Function Entity.
    /// </summary>
    [MetadataType(typeof(FunctionMetadata))]
    public class Function : Reference
    {
    }
}