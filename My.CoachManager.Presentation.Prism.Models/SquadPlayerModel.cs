﻿using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Presentation.Prism.Models
{
    [MetadataType(typeof(SquadPlayerMetadata))]
    public class SquadPlayerModel : PlayerModel
    {
        /// <summary>
        /// Gets or sets the player's squad id.
        /// </summary>
        public int SquadId { get; set; }

        /// <summary>
        /// Gets or sets the player's squad.
        /// </summary>
        public SquadModel Squad { get; set; }

        /// <summary>
        /// Gets or sets the default player's number in the roster.
        /// </summary>
        public int? Number { get; set; }

        /// <summary>
        /// Gets or sets the license state.
        /// </summary>
        public LicenseState LicenseState { get; set; }

        /// <summary>
        /// Gets or sets a value indicates if the player is in mutation.
        /// </summary>
        public bool IsMutation { get; set; }
    }
}