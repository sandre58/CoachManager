using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Models
{
    [MetadataType(typeof(PersonMetadata))]
    public abstract class PersonModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PersonModel"/>.
        /// </summary>
        public PersonModel()
        {
            Gender = PlayerConstants.DefaultGender;
            Emails = new ContactsCollection<EmailModel>();
            Phones = new ContactsCollection<PhoneModel>();
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
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
        public CountryModel Country { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public int? AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the license number.
        /// </summary>
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the size for clothes.
        /// </summary>
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        public ContactsCollection<PhoneModel> Phones { get; set; }

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        public ContactsCollection<EmailModel> Emails { get; set; }

        /// <summary>
        /// Get the full name (FirstName LastName).
        /// </summary>
        [Display(Name = "FullName", ResourceType = typeof(PersonResources))]
        public string FullName
        {
            get { return string.Join(" ", FirstName, LastName); }
        }

        /// <summary>
        /// Get the inverse name (LastName FirstName).
        /// </summary>
        public string InverseName => string.Join(" ", LastName, FirstName);

        /// <summary>
        /// Get the full name.
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(PersonResources))]
        public string FullAddress => Address ?? string.Empty;

        /// <summary>
        /// Get the default phone.
        /// </summary>
        public string Phone
        {
            get
            {
                if (Phones == null || Phones.Count <= 0) return string.Empty;
                var defaultPhone = Phones.FirstOrDefault(p => p.Default);
                return defaultPhone != null ? defaultPhone.Value : Phones.First().Value;
            }
        }

        /// <summary>
        /// Get the default email.
        /// </summary>
        public string Email
        {
            get
            {
                if (Emails == null || Emails.Count <= 0) return string.Empty;
                var defaultEmail = Emails.FirstOrDefault(p => p.Default);
                return defaultEmail != null ? defaultEmail.Value : Emails.First().Value;
            }
        }

        /// <summary>
        /// Get the current age.
        /// </summary>
        [Display(Name = "Age", ResourceType = typeof(PersonResources))]
        public int? Age
        {
            get
            {
                if (!Birthdate.HasValue) return null;

                var today = DateTime.UtcNow;
                var birthdate = Birthdate.Value;

                // Calculate the age.
                var age = today.Year - birthdate.Year;

                // Do stuff with it.
                if (today.Month < birthdate.Month || (today.Month == birthdate.Month && today.Day < birthdate.Day))
                    age--;

                return age;
            }
        }
    }
}