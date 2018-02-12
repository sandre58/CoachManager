using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Application.Dtos.Persons
{
    /// <summary>
    /// Data Transfer Object for Person item.
    /// </summary>
    [DataContract]
    [KnownType(typeof(PlayerDto))]
    [KnownType(typeof(CoachDto))]
    public class PersonDto : EntityDto
    {
        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public DateTime? Birthdate { get; set; }

        [DataMember]
        public string PlaceOfBirth { get; set; }

        [DataMember]
        public int? CountryId { get; set; }

        [DataMember]
        public byte[] Photo { get; set; }

        [DataMember]
        public GenderType Gender { get; set; }

        [DataMember]
        public int? AddressId { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string PostalCode { get; set; }

        [DataMember]
        public string City { get; set; }

        [DataMember]
        public string LicenseNumber { get; set; }

        [DataMember]
        public string Description { get; set; }

        [DataMember]
        public string Size { get; set; }

        [DataMember]
        public IEnumerable<PhoneDto> Phones { get; set; }

        [DataMember]
        public IEnumerable<EmailDto> Emails { get; set; }
    }
}