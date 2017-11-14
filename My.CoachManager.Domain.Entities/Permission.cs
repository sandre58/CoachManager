using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(PermissionMetadata))]
    public class Permission : DataEntity
    {
        public Permission()
        {
            Roles = new HashSet<Role>();
        }

        public ICollection<Role> Roles { get; set; }
    }
}