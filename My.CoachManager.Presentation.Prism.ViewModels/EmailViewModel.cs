using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Email Entity.
    /// </summary>
    [MetadataType(typeof(EmailMetadata))]
    public class EmailViewModel : ContactViewModel
    {
    }
}