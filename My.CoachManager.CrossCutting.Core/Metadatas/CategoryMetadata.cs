using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides Metadata for a Category Entity.
    /// </summary>
    public class CategoryMetadata : DataEntityMetadata
    {
        [Display(Name = "Year", ResourceType = typeof(CategoryResources))]
        public int? Year { get; set; }
    }
}