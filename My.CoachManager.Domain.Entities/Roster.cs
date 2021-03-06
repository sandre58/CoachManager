﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Roster Entity.
    /// </summary>
    [Serializable]
    public class Roster : Entity
    {
        /// <summary>
        /// Initialize a new instance of <see cref="Roster"/>.
        /// </summary>
        public Roster()
        {
            Players = new List<RosterPlayer>();
            Squads = new List<Squad>();
        }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the season id.
        /// </summary>
        [Required]
        public int SeasonId { get; set; }

        /// <summary>
        /// Gets or sets the season.
        /// </summary>
        public virtual Season Season { get; set; }

        /// <summary>
        /// Gets or sets the squad's category id.
        /// </summary>
        [Required]
        public int CategoryId { get; set; }

        /// <summary>
        /// Gets or sets the squad's category.
        /// </summary>
        public virtual Category Category { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        public ICollection<RosterPlayer> Players { get; set; }

        /// <summary>
        /// Gets or set the squads.
        /// </summary>
        public ICollection<Squad> Squads { get; set; }
    }
}
