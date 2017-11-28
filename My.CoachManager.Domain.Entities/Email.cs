using My.CoachManager.CrossCutting.Core.Metadatas;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Email Entity.
    /// </summary>
    [MetadataType(typeof(EmailMetadata))]
    public class Email : Contact
    {
    }
}