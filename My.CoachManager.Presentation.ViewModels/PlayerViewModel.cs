using My.CoachManager.CrossCutting.Core.Enums;
using System;
using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Core.ViewModels;
using System.Linq;

namespace My.CoachManager.Presentation.ViewModels
{
    [MetadataType(typeof(PlayerMetadata))]
    public class PlayerViewModel : EntityViewModel
    {
        public PlayerViewModel()
        {
            Gender = PlayerConstants.DefaultGender;
            Laterality = PlayerConstants.DefaultLaterality;
            LicenseState = PlayerConstants.DefaultLicenseState;
            Mutation = PlayerConstants.DefaultMutation;
            Phones = new ContactsCollection<PhoneViewModel>();
            Emails = new ContactsCollection<EmailViewModel>();
        }

        private string _lastName;
        public string LastName { get { return _lastName; } set { SetEntityProperty(() => _lastName = value, value, LastName); } }

        private string _firstName;
        public string FirstName { get { return _firstName; } set { SetEntityProperty(() => _firstName = value, value, FirstName); } }

        private DateTime? _birthdate;
        public DateTime? Birthdate { get { return _birthdate; } set { SetEntityProperty(() => _birthdate = value, value, Birthdate); } }

        private string _placeOfBirth;
        public string PlaceOfBirth { get { return _placeOfBirth; } set { SetEntityProperty(() => _placeOfBirth = value, value, PlaceOfBirth); } }

        private byte[] _photo;
        public byte[] Photo { get { return _photo; } set { SetEntityProperty(() => _photo = value, value, Photo); } }

        private string _address;
        public string Address { get { return _address; } set { SetEntityProperty(() => _address = value, value, Address); } }

        private string _postalCode;
        public string PostalCode { get { return _postalCode; } set { SetEntityProperty(() => _postalCode = value, value, PostalCode); } }

        private string _city;
        public string City { get { return _city; } set { SetEntityProperty(() => _city = value, value, City); } }

        private GenderType _gender;
        public GenderType Gender { get { return _gender; } set { SetEntityProperty(() => _gender = value, value, Gender); } }

        private Laterality _laterality;
        public Laterality Laterality { get { return _laterality; } set { SetEntityProperty(() => _laterality = value, value, _laterality); } }

        private string _licenseNumber;
        public string LicenseNumber { get { return _licenseNumber; } set { SetEntityProperty(() => _licenseNumber = value, value, LicenseNumber); } }

        private LicenseState _licenseState;
        public LicenseState LicenseState { get { return _licenseState; } set { SetEntityProperty(() => _licenseState = value, value, LicenseState); } }

        private bool _mutation;
        public bool Mutation { get { return _mutation; } set { SetEntityProperty(() => _mutation = value, value, Mutation); } }

        private int? _number;
        public int? Number { get { return _number; } set { SetEntityProperty(() => _number = value, value, Number); } }

        private int? _shoesSize;
        public int? ShoesSize { get { return _shoesSize; } set { SetEntityProperty(() => _shoesSize = value, value, ShoesSize); } }

        private string _size;
        public string Size { get { return _size; } set { SetEntityProperty(() => _size = value, value, Size); } }

        private int? _height;
        public int? Height { get { return _height; } set { SetEntityProperty(() => _height = value, value, Height); } }

        private int? _weight;
        public int? Weight { get { return _weight; } set { SetEntityProperty(() => _weight = value, value, Weight); } }

        private string _description;
        public string Description { get { return _description; } set { SetEntityProperty(() => _description = value, value, Description); } }

        private int? _categoryId;
        public int? CategoryId { get { return _categoryId; } set { SetEntityProperty(() => _categoryId = value, value, CategoryId); } }

        private int? _countryId;
        public int? CountryId { get { return _countryId; } set { SetEntityProperty(() => _countryId = value, value, CountryId); } }

        private CategoryViewModel _category;
        public CategoryViewModel Category { get { return _category; } set { SetEntityProperty(() => _category = value, value, Category); } }

        private CountryViewModel _country;
        public CountryViewModel Country { get { return _country; } set { SetEntityProperty(() => _country = value, value, Country); } }

        private ContactsCollection<PhoneViewModel> _phones;
        public ContactsCollection<PhoneViewModel> Phones { get { return _phones; } set { SetEntityProperty(() => _phones = value, value, Phones); } }

        private ContactsCollection<EmailViewModel> _email;
        public ContactsCollection<EmailViewModel> Emails { get { return _email; } set { SetEntityProperty(() => _email = value, value, Emails); } }

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
        /// Get the full name.
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