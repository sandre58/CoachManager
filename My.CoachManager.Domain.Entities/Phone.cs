using My.CoachManager.CrossCutting.Core.Metadatas;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Phone Entity.
    /// </summary>
    [MetadataType(typeof(PhoneMetadata))]
    public class Phone : Contact
    {
    }
}