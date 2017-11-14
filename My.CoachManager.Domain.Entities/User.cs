using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [MetadataType(typeof(UserMetadata))]
    public class User : Entity
    {
        public User()
        {
            Roles = new HashSet<Role>();
        }

        public string Name { get; set; }

        public string Login { get; set; }

        public string Password { get; set; }

        public string Mail { get; set; }

        public ICollection<Role> Roles { get; set; }
    }
}