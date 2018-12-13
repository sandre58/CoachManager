﻿using System.Collections.Generic;
using System.Runtime.Serialization;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos
{
    /// <summary>
    /// Data Transfer Object for Player item.
    /// </summary>
    [DataContract]
    public class PlayerDto : PersonDto
    {
        [DataMember]
        public int? CategoryId { get; set; }

        [DataMember]
        public CategoryDto Category { get; set; }

        [DataMember]
        public Laterality Laterality { get; set; }

        [DataMember]
        public int? Height { get; set; }

        [DataMember]
        public int? Weight { get; set; }

        [DataMember]
        public int? ShoesSize { get; set; }

        /// <summary>
        /// Gets or set the players.
        /// </summary>
        [DataMember]
        public IEnumerable<PlayerPositionDto> Positions { get; set; }
    }
}