using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Domain.Core;

namespace My.CoachManager.Domain.Entities
{
    /// <summary>
    /// Provides properties for a Person Entity.
    /// </summary>
    public abstract class Person : Entity
    {
        /// <summary>
        /// Initalize a new instance of <see cref="Person"/>.
        /// </summary>
        protected Person()
        {
            Gender = PlayerConstants.DefaultGender;
            Contacts = new List<Contact>();
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Required]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Required]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the place of birth.
        /// </summary>
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the nationality id.
        /// </summary>
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        public Country Country { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [Required]
        public GenderType Gender { get; set; }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public Address Address { get; set; }

        /// <summary>
        /// Gets or sets the license number.
        /// </summary>
        [MaxLength(10)]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the date of arrival to club.
        /// </summary>
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the size for clothes.
        /// </summary>
        [MaxLength(4)]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the contacts.
        /// </summary>
        public ICollection<Contact> Contacts { get; set; }
    }
}