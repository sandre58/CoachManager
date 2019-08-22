using System;

using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Category Entity.
    /// </summary>
    [Serializable]
    public class Category : Reference
    {
        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        public int? Age { get; set; }
    }
}
