using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Country Entity.
    /// </summary>
    public class CountryMetadata : ReferenceMetadata
    {
        [Display(Name = "Flag", ResourceType = typeof(CountryResources))]
        public string Flag { get; set; }
    }
}