using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    /// <summary>
    /// Provides metadata for a Role Entity.
    /// </summary>
    public class RoleMetadata : ReferenceMetadata
    {
        [Display(Name = "Permissions", ResourceType = typeof(RoleResources))]
        public object Permissions { get; set; }

        [Display(Name = "Users", ResourceType = typeof(RoleResources))]
        public object Users { get; set; }
    }
}