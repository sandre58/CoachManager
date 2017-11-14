using System;

namespace My.CoachManager.Domain.Core
{
    /// <summary>
    /// Services to allow changes to an entity to be tracked.
    /// </summary>
    public interface IAuditable
    {
        #region Properties

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

        #endregion Properties
    }
}