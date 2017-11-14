using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(RoleMetadata))]
    public class Role : DataEntity
    {
        public Role()
        {
            Permissions = new HashSet<Permission>();
            Users = new HashSet<User>();
        }

        public ICollection<Permission> Permissions { get; set; }

        public ICollection<User> Users { get; set; }
    }
}