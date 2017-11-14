using System;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    public abstract class EntityBase : IAuditable, IEntityBase
    {
        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Gets or sets the created user.
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// Gets or sets the updated date.
        /// </summary>
        public DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Gets or sets the updated user.
        /// </summary>
        public string ModifiedBy { get; set; }
    }
}