﻿using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Category Entity.
    /// </summary>
    public class Category : Reference
    {
        /// <summary>
        /// Gets or sets the Year.
        /// </summary>
        public int? Year { get; set; }
    }
}