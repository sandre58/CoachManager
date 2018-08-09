using System.ComponentModel.DataAnnotations;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a User Entity.
    /// </summary>
    public class User : Entity
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        [MaxLength(150)]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the login.
        /// </summary>
        [Required]
        [MaxLength(100)]
        public string Login { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        [Required]
        public string Password { get; set; }

        /// <summary>
        /// Gets or sets the mail.
        /// </summary>
        [Required]
        [MaxLength(200)]
        public string Mail { get; set; }
    }
}