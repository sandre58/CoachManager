using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Role Entity.
    /// </summary>
    [MetadataType(typeof(RoleMetadata))]
    public class Role : DataEntity
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Role"/>.
        /// </summary>
        public Role()
        {
            Permissions = new List<Permission>();
            Users = new List<User>();
        }

        /// <summary>
        /// Gets or sets the permissions
        /// </summary>
        public ICollection<Permission> Permissions { get; set; }

        /// <summary>
        /// Gets or sets the users.
        /// </summary>
        public ICollection<User> Users { get; set; }
    }
}