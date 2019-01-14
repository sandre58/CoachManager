using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.AddressServiceReference;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PositionServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Core.ViewModels
{

    public class PlayerEditViewModel : PlayerEditViewModel<PlayerModel>
    {

        #region Fields

        private readonly IPersonService _personService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerEditViewModel"/>.
        /// </summary>
        public PlayerEditViewModel(IPersonService personService, ICategoryService categoryService, IAddressService addressService, IPositionService positionService) : base(personService, categoryService, addressService, positionService)
        {
            _personService = personService;
        }

        #endregion Constructors

        #region Methods

        #region Data

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerModel LoadItemCore(int id)
        {
            return PlayerFactory.Get(_personService.GetPlayerById(id));
        }

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override int SaveItemCore()
        {
            return _personService.SavePlayer(PlayerFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
        }

        #endregion Data

        #endregion Methods

    }

    public abstract class PlayerEditViewModel<TPlayerEntity> : EditViewModel<TPlayerEntity>
        where TPlayerEntity : PlayerModel, new()
    {
        #region Constants

        public const string DefaultCountry = "France";

        #endregion Constants

        #region Fields

        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private readonly IAddressService _addressService;
        private readonly IPositionService _positionService;

        #endregion Fields

        #region Constructors

        /// <summary>
            /// Initialise a new instance of <see cref="PlayerEditViewModel{T}"/>.
            /// </summary>
            protected PlayerEditViewModel(IPersonService personService, ICategoryService categoryService, IAddressService addressService, IPositionService positionService)
        {
            _personService = personService;
            _categoryService = categoryService;
            _addressService = addressService;
            _positionService = positionService;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets addresses.
        /// </summary>
        public IEnumerable<CityModel> AllAdresses { get; private set; }

        /// <summary>
        /// Gets or sets cities.
        /// </summary>
        public IEnumerable<string> AllCities { get; private set; }

        /// <summary>
        /// Gets or sets postal code.
        /// </summary>
        public IEnumerable<string> AllPostalCodes { get; private set; }

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryModel> AllCategories { get; private set; }

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<string> AllSizes { get; private set; }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public IEnumerable<CountryModel> AllCountries { get; private set; }

        /// <summary>
        /// Gets or sets phone labels.
        /// </summary>
        public IEnumerable<string> AllPhoneLabels { get; set; }

        /// <summary>
        /// Gets or sets email labels.
        /// </summary>
        public IEnumerable<string> AllEmailLabels { get; set; }

        /// <summary>
        /// Gets phones.
        /// </summary>
        public ObservableItemsCollection<PhoneModel> Phones { get; private set; }

        /// <summary>
        /// Gets phones.
        /// </summary>
        public ObservableItemsCollection<EmailModel> Emails { get; private set; }

        /// <summary>
        /// Gets or sets positions.
        /// </summary>
        public IEnumerable<PositionModel> AllPositions { get; private set; }

        /// <summary>
        /// Get or Set Select Photo Command.
        /// </summary>
        public DelegateCommand SelectPhotoCommand { get; private set; }

        /// <summary>
        /// Get or Set Remove Photo Command.
        /// </summary>
        public DelegateCommand RemovePhotoCommand { get; private set; }

        /// <summary>
        /// Get or Set Add Phone Command.
        /// </summary>
        public DelegateCommand<PhoneModel> AddPhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Phone Command.
        /// </summary>
        public DelegateCommand<PhoneModel> RemovePhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Add Email Command.
        /// </summary>
        public DelegateCommand<EmailModel> AddEmailCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Email Command.
        /// </summary>
        public DelegateCommand<EmailModel> RemoveEmailCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Phone Command.
        /// </summary>
        public DelegateCommand<PositionModel> RemovePositionCommand { get; set; }

        #endregion Members

        #region Methods

        #region Initialization

        /// <summary>
        /// Initializes data in constructor.
        /// </summary>
        protected override void InitializeData()
        {
            base.InitializeData();

            AllPhoneLabels = ContactConstants.DefaultPhoneLabels;
            AllEmailLabels = ContactConstants.DefaultEmailLabels;

            Emails = new ObservableItemsCollection<EmailModel>();
            Phones = new ObservableItemsCollection<PhoneModel>();

            Phones.CollectionChanged += Contacts_CollectionChanged;
            Emails.CollectionChanged += Contacts_CollectionChanged;

            AddEmail(null);
            AddPhone(null);
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommand()
        {
            base.InitializeCommand();

            AddEmailCommand = new DelegateCommand<EmailModel>(AddEmail, CanAddEmail);
            RemoveEmailCommand = new DelegateCommand<EmailModel>(RemoveEmail, CanRemoveEmail);
            AddPhoneCommand = new DelegateCommand<PhoneModel>(AddPhone, CanAddPhone);
            RemovePhoneCommand = new DelegateCommand<PhoneModel>(RemovePhone, CanRemovePhone);
            SelectPhotoCommand = new DelegateCommand(SelectPhoto, CanSelectPhoto);
            RemovePhotoCommand = new DelegateCommand(RemovePhoto, CanRemovePhoto);
            RemovePositionCommand = new DelegateCommand<PositionModel>(RemovePosition, CanRemovePosition);
        }

        #endregion Initialization

        #region Select Photo

        /// <summary>
        /// Select the photo.
        /// </summary>
        public void SelectPhoto()
        {
            var filename = DialogManager.ShowOpenImagesDialog();

            if (!string.IsNullOrEmpty(filename))
            {
                Item.Photo = File.ReadAllBytes(filename);
            }
        }

        /// <summary>
        /// Can select the photo ?
        /// </summary>
        /// <returns></returns>
        public bool CanSelectPhoto()
        {
            return true;
        }

        #endregion Select Photo

        #region Remove Photo

        /// <summary>
        /// Delete the photo.
        /// </summary>
        public void RemovePhoto()
        {
            Item.Photo = null;
        }

        /// <summary>
        /// Can delete the photo ?
        /// </summary>
        /// <returns></returns>
        public bool CanRemovePhoto()
        {
            return Item.Photo != null && Item.Photo.Length > 0;
        }

        #endregion Remove Photo

        #region Contacts

        #region Phones

        /// <summary>
        /// Add a contact.
        /// </summary>
        private void AddPhone(PhoneModel phone)
        {
            Phones.Add(new PhoneModel());
        }

        /// <summary>
        /// Can Add a contact.
        /// </summary>
        private bool CanAddPhone(PhoneModel phone)
        {
            return Phones.IndexOf(phone) == Phones.Count - 1 && !string.IsNullOrEmpty(phone.Value);
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        private void RemovePhone(PhoneModel phone)
        {
            if (Phones.Count > 1)
                Phones.Remove(phone);
            else if (Phones.Count == 1)
            {
                phone.Label = "";
                phone.Value = "";
            }
        }

        /// <summary>
        /// Can remove a contact.
        /// </summary>
        private bool CanRemovePhone(PhoneModel phone)
        {
            return Phones.Count > 1 || !string.IsNullOrEmpty(phone.Value);
        }

        #endregion

        #region Emails

        /// <summary>
        /// Add a contact.
        /// </summary>
        private void AddEmail(EmailModel mail)
        {
            Emails.Add(new EmailModel());
        }

        /// <summary>
        /// Can Add a contact.
        /// </summary>
        private bool CanAddEmail(EmailModel mail)
        {
            return Emails.IndexOf(mail) == Emails.Count - 1 && !string.IsNullOrEmpty(mail.Value);
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        private void RemoveEmail(EmailModel email)
        {
            if (email != null)
            {
                if (Emails.Count > 1)
                    Emails.Remove(email);
                else if (Phones.Count == 1)
                {
                    email.Label = "";
                    email.Value = "";
                }
            }
        }

        /// <summary>
        /// Can remove a contact.
        /// </summary>
        private bool CanRemoveEmail(EmailModel mail)
        {
            return Emails.Count > 1 || !string.IsNullOrEmpty(mail.Value);
        }

        #endregion

        private void Contacts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateContactsCommand();
        }

        private void UpdateContactsCommand()
        {
            RemoveEmailCommand?.RaiseCanExecuteChanged();
            RemovePhoneCommand?.RaiseCanExecuteChanged();
            AddEmailCommand?.RaiseCanExecuteChanged();
            AddPhoneCommand?.RaiseCanExecuteChanged();
        }

        #endregion

        #region  Positions

        /// <summary>
        /// Raises when positions changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Position_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            Item.Positions = AllPositions.Where(x => x.IsSelected)
                .Select(x => Item.Positions.Any(y => y.PositionId == x.Id) ? Item.Positions.First(y => y.PositionId == x.Id) : PlayerFactory.CreatePosition(x)).ToObservableCollection();
        }

        /// <summary>
        /// Remove a position.
        /// </summary>
        /// <param name="position"></param>
        protected void RemovePosition(PositionModel position)
        {
            AllPositions.Where(x => x.Equals(position)).ForEach(x => x.IsSelected = false);
        }

        /// <summary>
        /// Can remove a position.
        /// </summary>
        private bool CanRemovePosition(PositionModel position)
        {
            return position != null;
        }

        #endregion

        #region Data

        protected override void InitializeDataCore()
        {
            AllCategories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            AllCountries = _personService.GetCountries().Select(CountryFactory.Get);
            AllAdresses = _addressService.GetCities().Select(AddressFactory.Get).ToList();
            AllPositions = _positionService.GetPositions().Select(PositionFactory.Get).ToList();
            AllSizes = PlayerConstants.DefaultSizes;

            AllCities = AllAdresses.Select(c => c.City).Distinct().OrderBy(x => x);
            AllPostalCodes = AllAdresses.Select(c => c.PostalCode).Distinct().OrderBy(x => x);

        }

        /// <summary>
        /// Called after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            if (Mode == ScreenMode.Creation)
            {
                var country = AllCountries?.FirstOrDefault(c => c.Label == DefaultCountry);

                if (country != null) Item.CountryId = country.Id;
            }

            if (Item != null)
            {
                Item.PropertyChanged += OnItemPropertyChanged;

                if (Item.Emails.Count != 0)
                {
                    Emails = Item.Emails;
                    Emails.CollectionChanged += Contacts_CollectionChanged;
                }
                if (Item.Phones.Count != 0)
                {
                    Phones = Item.Phones;
                    Phones.CollectionChanged += Contacts_CollectionChanged;
                }

                AllPositions.ForEach(x => x.PropertyChanged -= Position_PropertyChanged);
                AllPositions.ForEach(x => x.IsSelected = Item.Positions.Any(y => y.PositionId == x.Id));
                AllPositions.ForEach(x => x.PropertyChanged += Position_PropertyChanged);
            }

            base.OnLoadDataCompleted();
        }

        /// <summary>
        /// Called before save.
        /// </summary>
        protected override void OnSaveRequested()
        {
            Item.Emails = Emails.Where(x => !string.IsNullOrEmpty(x.Value)).ToItemsObservableCollection();
            Item.Phones = Phones.Where(x => !string.IsNullOrEmpty(x.Value)).ToItemsObservableCollection();

            base.OnSaveRequested();
        }

        #endregion Data

        #region Properties Changed

        /// <summary>
        /// Calls when Item changed.
        /// </summary>
        protected override void OnItemChanged()
        {
            base.OnItemChanged();

            RemovePhotoCommand?.RaiseCanExecuteChanged();
            SelectPhotoCommand?.RaiseCanExecuteChanged();
            UpdateContactsCommand();
        }

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "City":
                    if (!string.IsNullOrEmpty(Item.City))
                    {
                        Item.PostalCode = AllAdresses.Where(c => c.City == Item.City).Select(c => c.PostalCode).FirstOrDefault();
                    }
                    break;

                case "PostalCode":
                    if (!string.IsNullOrEmpty(Item.PostalCode))
                    {
                        Item.City = AllAdresses.Where(c => c.PostalCode == Item.PostalCode).Select(c => c.City).FirstOrDefault();
                    }
                    break;

                case "Birthdate":
                    if (Item.Birthdate.HasValue)
                    {
                        var category = _personService.GetCategoryFromBirthdate(Item.Birthdate.Value);
                        Item.CategoryId = category?.Id ?? 0;
                    }
                    break;

                case "Photo":
                    RemovePhotoCommand.RaiseCanExecuteChanged();
                    break;
            }
        }

        #endregion Properties Changed

        #endregion Methods
    }
}