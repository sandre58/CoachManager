﻿using System;
using System.ComponentModel.DataAnnotations;

using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Contact Entity.
    /// </summary>
    [Serializable]
    public abstract class Contact : Entity
    {
        /// <summary>
        /// Gets or sets the label.
        /// </summary>
        public string Label { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if this contact is the default contact.
        /// </summary>
        public bool Default { get; set; }

        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        [Required]
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the person id.
        /// </summary>
        [Required]
        public int PersonId { get; set; }

        /// <summary>
        /// Gets or sets the person.
        /// </summary>
        public virtual Person Person { get; set; }

        /// <summary>
        /// Gets or sets the Business identifier.
        /// </summary>
        public override string BusinessKey => Value;
    }
}
