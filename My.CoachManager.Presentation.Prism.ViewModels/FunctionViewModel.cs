using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Function item.
    /// </summary>
    [MetadataType(typeof(FunctionMetadata))]
    public class FunctionViewModel : DataEntityViewModel
    {
    }
}