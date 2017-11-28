using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Phone Entity.
    /// </summary>
    [MetadataType(typeof(PhoneMetadata))]
    public class PhoneViewModel : ContactViewModel
    {
    }
}