using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class CountryMetadata : DataEntityMetadata
    {
        [Display(Name = "Flag", ResourceType = typeof(CountryResources))]
        public string Flag { get; set; }
    }
}