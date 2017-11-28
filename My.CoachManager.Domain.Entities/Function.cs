using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Function Entity.
    /// </summary>
    [MetadataType(typeof(FunctionMetadata))]
    public class Function : DataEntity
    {
    }
}