using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Resources;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Core.Attributes.Validation;
using My.CoachManager.Presentation.Core.Models;

namespace My.CoachManager.Presentation.Models
{
    public abstract class PersonModel : EntityModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PersonModel"/>.
        /// </summary>
        public PersonModel()
        {
            Gender = PlayerConstants.DefaultGender;
            Emails = new ObservableItemsCollection<EmailModel>();
            Phones = new ObservableItemsCollection<PhoneModel>();

            Rules.Add(nameof(Address), ValidationMessageResources.IncompleteAddressMesage, o =>
            {
                var item = (PersonModel)o;
                return !(string.IsNullOrEmpty(item.Address) &&
                (!string.IsNullOrEmpty(item.PostalCode) || !string.IsNullOrEmpty(item.City)));
            });

            Rules.Add(nameof(PostalCode), ValidationMessageResources.IncompleteAddressMesage, o =>
            {
                var item = (PersonModel)o;
                return !(string.IsNullOrEmpty(item.PostalCode) &&
                       (!string.IsNullOrEmpty(item.Address) || !string.IsNullOrEmpty(item.City)));
            });

            Rules.Add(nameof(City), ValidationMessageResources.IncompleteAddressMesage, o =>
            {
                var item = (PersonModel)o;
                return !(string.IsNullOrEmpty(item.City) &&
                       (!string.IsNullOrEmpty(item.Address) || !string.IsNullOrEmpty(item.PostalCode)));
            });
        }

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        [Display(Name = "LastName", ResourceType = typeof(PersonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string LastName { get; set; }

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        [Display(Name = "FirstName", ResourceType = typeof(PersonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string FirstName { get; set; }

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        [Display(Name = "Birthdate", ResourceType = typeof(PersonResources))]
        public DateTime? Birthdate { get; set; }

        /// <summary>
        /// Gets or sets the FromDate.
        /// </summary>
        [Display(Name = "FromDate", ResourceType = typeof(PersonResources))]
        public DateTime? FromDate { get; set; }

        /// <summary>
        /// Gets or sets the place of birth.
        /// </summary>
        [Display(Name = "PlaceOfBirth", ResourceType = typeof(PersonResources))]
        public string PlaceOfBirth { get; set; }

        /// <summary>
        /// Gets or sets the nationality id.
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(PersonResources))]
        public int? CountryId { get; set; }

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        [Display(Name = "Country", ResourceType = typeof(PersonResources))]
        public CountryModel Country { get; set; }

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        [Display(Name = "Photo", ResourceType = typeof(PersonResources))]
        public byte[] Photo { get; set; }

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        [Display(Name = "Gender", ResourceType = typeof(PersonResources))]
        [Required(ErrorMessageResourceName = "RequiredFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public GenderType Gender { get; set; }

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(PersonResources))]
        public int? AddressId { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(PersonResources))]
        [ValidateProperty(nameof(City))]
        [ValidateProperty(nameof(PostalCode))]
        public string Address { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Display(Name = "PostalCode", ResourceType = typeof(AddressResources))]
        [MaxLength(5, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        [ValidateProperty(nameof(Address))]
        [ValidateProperty(nameof(City))]
        public string PostalCode { get; set; }

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        [Display(Name = "City", ResourceType = typeof(AddressResources))]
        [ValidateProperty(nameof(Address))]
        [ValidateProperty(nameof(PostalCode))]
        public string City { get; set; }

        /// <summary>
        /// Gets or sets the license number.
        /// </summary>
        [Display(Name = "LicenseNumber", ResourceType = typeof(PersonResources))]
        [MaxLength(10, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string LicenseNumber { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        [Display(Name = "Description", ResourceType = typeof(PersonResources))]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the size for clothes.
        /// </summary>
        [Display(Name = "Size", ResourceType = typeof(PersonResources))]
        [MaxLength(4, ErrorMessageResourceName = "MaxLenghtFieldMessage", ErrorMessageResourceType = typeof(ValidationMessageResources))]
        public string Size { get; set; }

        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        [Display(Name = "Phones", ResourceType = typeof(PersonResources))]
        public ObservableItemsCollection<PhoneModel> Phones { get; set; }

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        [Display(Name = "Emails", ResourceType = typeof(PersonResources))]
        public ObservableItemsCollection<EmailModel> Emails { get; set; }

        /// <summary>
        /// Get the full name (FirstName LastName).
        /// </summary>
        [Display(Name = "FullName", ResourceType = typeof(PersonResources))]
        public string FullName => string.Join(" ", FirstName, LastName);

        /// <summary>
        /// Get the inverse name (LastName FirstName).
        /// </summary>
        [Display(Name = "FullName", ResourceType = typeof(PersonResources))]
        public string InverseName => string.Join(" ", LastName, FirstName);

        /// <summary>
        /// Get the full name.
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(PersonResources))]
        public string FullAddress => Address ?? string.Empty;

        /// <summary>
        /// Get the default phone.
        /// </summary>
        [Display(Name = "Phone", ResourceType = typeof(PersonResources))]
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
        [Display(Name = "Email", ResourceType = typeof(PersonResources))]
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
                if (today.Month < birthdate.Month || today.Month == birthdate.Month && today.Day < birthdate.Day)
                    age--;

                return age;
            }
        }

    }
}
