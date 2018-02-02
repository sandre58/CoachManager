using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.CrossCutting.Core.Resources.Entities;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PersonMetadata))]
    public abstract class PersonViewModel : EntityViewModel
    {
        /// <summary>
        /// Initialise a new instance of <see cref="PersonViewModel"/>.
        /// </summary>
        public PersonViewModel()
        {
            Gender = PlayerConstants.DefaultGender;
            Emails = new ContactsCollection<EmailViewModel>();
            Phones = new ContactsCollection<PhoneViewModel>();
        }

        private string _lastName;

        /// <summary>
        /// Gets or sets the last name.
        /// </summary>
        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _firstName;

        /// <summary>
        /// Gets or sets the first name.
        /// </summary>
        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private DateTime? _birthdate;

        /// <summary>
        /// Gets or sets the birthdate.
        /// </summary>
        public DateTime? Birthdate
        {
            get { return _birthdate; }
            set { SetProperty(ref _birthdate, value); }
        }

        private string _placeOfBirth;

        /// <summary>
        /// Gets or sets the place of birth.
        /// </summary>
        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set { SetProperty(ref _placeOfBirth, value); }
        }

        private int? _countryId;

        /// <summary>
        /// Gets or sets the nationality id.
        /// </summary>
        public int? CountryId
        {
            get { return _countryId; }
            set { SetProperty(ref _countryId, value); }
        }

        private CountryViewModel _country;

        /// <summary>
        /// Gets or sets the nationality.
        /// </summary>
        public CountryViewModel Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private byte[] _photo;

        /// <summary>
        /// Gets or sets the photo.
        /// </summary>
        public byte[] Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        private GenderType _gender;

        /// <summary>
        /// Gets or sets the gender.
        /// </summary>
        public GenderType Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private int? _addressId;

        /// <summary>
        /// Gets or sets the address id.
        /// </summary>
        public int? AddressId
        {
            get { return _addressId; }
            set { SetProperty(ref _addressId, value); }
        }

        private AddressViewModel _address;

        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        public AddressViewModel Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _licenseNumber;

        /// <summary>
        /// Gets or sets the license number.
        /// </summary>
        public string LicenseNumber
        {
            get { return _licenseNumber; }
            set { SetProperty(ref _licenseNumber, value); }
        }

        private string _description;

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _size;

        /// <summary>
        /// Gets or sets the size for clothes.
        /// </summary>
        public string Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }

        private ContactsCollection<PhoneViewModel> _phones;

        /// <summary>
        /// Gets or sets the phones.
        /// </summary>
        public ContactsCollection<PhoneViewModel> Phones { get { return _phones; } set { SetProperty(ref _phones, value); } }

        private ContactsCollection<EmailViewModel> _emails;

        /// <summary>
        /// Gets or sets the emails.
        /// </summary>
        public ContactsCollection<EmailViewModel> Emails { get { return _emails; } set { SetProperty(ref _emails, value); } }

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
        public string InverseName
        {
            get { return string.Join(" ", LastName, FirstName); }
        }

        /// <summary>
        /// Get the full name.
        /// </summary>
        [Display(Name = "Address", ResourceType = typeof(PersonResources))]
        public string FullAddress
        {
            get { return Address != null ? Address.ToString() : ""; }
        }

        /// <summary>
        /// Get the default phone.
        /// </summary>
        public string Phone
        {
            get
            {
                if (Phones.Count > 0)
                {
                    var defaultPhone = Phones.FirstOrDefault(p => p.Default);
                    return defaultPhone != null ? defaultPhone.Value : Phones.First().Value;
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Get the default email.
        /// </summary>
        public string Email
        {
            get
            {
                if (Emails.Count > 0)
                {
                    var defaultEmail = Emails.FirstOrDefault(p => p.Default);
                    return defaultEmail != null ? defaultEmail.Value : Emails.First().Value;
                }

                return string.Empty;
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