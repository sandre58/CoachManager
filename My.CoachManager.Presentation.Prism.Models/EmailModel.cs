using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.Models
{
    /// <summary>
    /// Provides properties for a Email Entity.
    /// </summary>
    [MetadataType(typeof(EmailMetadata))]
    public class EmailModel : ContactModel
    {
    }
}