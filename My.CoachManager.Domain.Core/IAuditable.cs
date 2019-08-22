using System;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Provides properties for an entity which is auditable.
    /// </summary>
    public interface IAuditable
    {
        /// <summary>
        /// Get or Set the creation date.
        /// </summary>
        DateTime? CreatedDate { get; set; }

        /// <summary>
        /// Get or Set the creation user.
        /// </summary>
        string CreatedBy { get; set; }

        /// <summary>
        /// Get or Set the modification date.
        /// </summary>
        DateTime? ModifiedDate { get; set; }

        /// <summary>
        /// Get or Set the modification user.
        /// </summary>
        string ModifiedBy { get; set; }
    }
}
