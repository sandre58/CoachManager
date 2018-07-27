using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Function item.
    /// </summary>
    [MetadataType(typeof(FunctionMetadata))]
    public class FunctionModel : ReferenceModel
    {
    }
}