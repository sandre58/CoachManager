﻿using System.Runtime.Serialization;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Contact Dto
    /// </summary>
    [DataContract]
    public class ContactDto : EntityDto
    {
        [DataMember]
        public string Label { get; set; }

        [DataMember]
        public string Value { get; set; }

        [DataMember]
        public bool Default { get; set; }

        [DataMember]
        public int PersonId { get; set; }
    }
}