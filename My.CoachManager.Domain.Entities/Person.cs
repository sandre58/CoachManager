using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;

namespace My.CoachManager.Domain.Entities
{
    [Table("Persons")]
    [MetadataType(typeof(PersonMetadata))]
    public abstract class Person : Entity
    {
        public Person()
        {
            Gender = PlayerConstants.DefaultGender;
            Emails = new HashSet<Email>();
            Phones = new HashSet<Phone>();
        }

        public string LastName { get; set; }

        public string FirstName { get; set; }

        public DateTime? Birthdate { get; set; }

        public string PlaceOfBirth { get; set; }

        public int? CountryId { get; set; }

        public Country Country { get; set; }

        public byte[] Photo { get; set; }

        public string Address { get; set; }

        public string PostalCode { get; set; }

        public string City { get; set; }

        public GenderType Gender { get; set; }

        public string LicenseNumber { get; set; }

        public string Description { get; set; }

        public string Size { get; set; }

        public ICollection<Email> Emails { get; set; }

        public ICollection<Phone> Phones { get; set; }
    }
}