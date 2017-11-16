using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    [MetadataType(typeof(PersonMetadata))]
    public abstract class PersonViewModel : EntityViewModel
    {
        public PersonViewModel()
        {
            Gender = PlayerConstants.DefaultGender;
            Contacts = new ContactsCollection<ContactViewModel>();
        }

        private string _lastName;

        public string LastName
        {
            get { return _lastName; }
            set { SetProperty(ref _lastName, value); }
        }

        private string _firstName;

        public string FirstName
        {
            get { return _firstName; }
            set { SetProperty(ref _firstName, value); }
        }

        private DateTime? _birthdate;

        public DateTime? Birthdate
        {
            get { return _birthdate; }
            set { SetProperty(ref _birthdate, value); }
        }

        private string _placeOfBirth;

        public string PlaceOfBirth
        {
            get { return _placeOfBirth; }
            set { SetProperty(ref _placeOfBirth, value); }
        }

        private int? _countryId;

        public int? CountryId
        {
            get { return _countryId; }
            set { SetProperty(ref _countryId, value); }
        }

        private CountryViewModel _country;

        public CountryViewModel Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private byte[] _photo;

        public byte[] Photo
        {
            get { return _photo; }
            set { SetProperty(ref _photo, value); }
        }

        private string _address;

        public string Address
        {
            get { return _address; }
            set { SetProperty(ref _address, value); }
        }

        private string _postalCode;

        public string PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }
        }

        private string _city;

        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }

        private GenderType _gender;

        public GenderType Gender
        {
            get { return _gender; }
            set { SetProperty(ref _gender, value); }
        }

        private string _licenseNumber;

        public string LicenseNumber
        {
            get { return _licenseNumber; }
            set { SetProperty(ref _licenseNumber, value); }
        }

        private string _description;

        public string Description
        {
            get { return _description; }
            set { SetProperty(ref _description, value); }
        }

        private string _size;

        public string Size
        {
            get { return _size; }
            set { SetProperty(ref _size, value); }
        }

        private ContactsCollection<ContactViewModel> _contacts;
        public ContactsCollection<ContactViewModel> Contacts { get { return _contacts; } set { SetProperty(ref _contacts, value); } }
        
        public ContactsCollection<PhoneViewModel> Phones { get { return new ContactsCollection<PhoneViewModel>(_contacts.OfType<PhoneViewModel>()); } }

        public ContactsCollection<EmailViewModel> Emails { get { return new ContactsCollection<EmailViewModel>(_contacts.OfType<EmailViewModel>()); } }

        /// <summary>
        /// Get the full name.
        /// </summary>
        public string FullName
        {
            get { return string.Join(" ", FirstName, LastName); }
        }

        /// <summary>
        /// Get the full name.
        /// </summary>
        public string InverseName
        {
            get { return string.Join(" ", LastName, FirstName); }
        }

        /// <summary>
        /// Get the full name.
        /// </summary>
        public string FullAddress
        {
            get { return string.Join(" ", Address, PostalCode, City != null ? City.ToUpper() : ""); }
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
        /// Get the age.
        /// </summary>
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