using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a User Entity.
    /// </summary>
    [MetadataType(typeof(UserMetadata))]
    public class User : Entity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Person"/>.
        /// </summary>
        public User()
        {
            Roles = new List<Role>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        public string Mail { get; set; }

        /// <summary>
        /// Gets or sets the roles.
        /// </summary>
        public ICollection<Role> Roles { get; set; }
    }
}