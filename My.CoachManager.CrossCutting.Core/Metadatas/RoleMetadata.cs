using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Entities;

namespace My.CoachManager.CrossCutting.Core.Metadatas
{
    public class RoleMetadata : DataEntityMetadata
    {
        [Display(Name = "Permissions", ResourceType = typeof(RoleResources))]
        public object Permissions { get; set; }

        [Display(Name = "Users", ResourceType = typeof(RoleResources))]
        public object Users { get; set; }
    }
}