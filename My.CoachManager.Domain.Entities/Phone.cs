using My.CoachManager.CrossCutting.Core.Metadatas;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(PhoneMetadata))]
    public class Phone : Contact
    {
    }
}