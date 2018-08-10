using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Manager;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.Models;
using My.CoachManager.Presentation.Prism.Models.Aggregates;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public partial class PlayerEditViewModel : EditViewModel<PlayerModel>
    {
        #region Constants

        public const string DefaultCountry = "France";

        #endregion Constants

        #region Fields

        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerEditViewModel"/>.
        /// </summary>
        public PlayerEditViewModel(IPersonService personService, ICategoryService categoryService)
        {
            _personService = personService;
            _categoryService = categoryService;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryModel> AllCategories { get; set; }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public IEnumerable<CountryModel> AllCountries { get; set; }

        /// <summary>
        /// Gets or sets cities names.
        /// </summary>
        public IEnumerable<string> AllCitiesNames { get; set; }

        /// <summary>
        /// Gets or sets postal codes.
        /// </summary>
        public IEnumerable<string> AllPostalCodes { get; set; }

        /// <summary>
        /// Get or Set Select Photo Command.
        /// </summary>
        public DelegateCommand SelectPhotoCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Photo Command.
        /// </summary>
        public DelegateCommand RemovePhotoCommand { get; set; }

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

            Emails = new ItemsObservableCollection<EmailModel>();
            Phones = new ItemsObservableCollection<PhoneModel>();

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

        #region Data

        protected override void InitializeDataCore()
        {
            AllCategories = _categoryService.GetCategories().Select(CategoryFactory.Get);
            AllCountries = _personService.GetCountries().Select(CountryFactory.Get);

            // TODO : Address
            //AllCities = new List<CityViewModel>();
            //AllCitiesNames = AllCities.Select(c => c.City).OrderBy(c => c);
            //AllPostalCodes = AllCities.Select(c => c.PostalCode).OrderBy(c => c);
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

            }

            base.OnLoadDataCompleted();
        }

        /// <inheritdoc />
        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerModel LoadItemCore(int id)
        {
            return PlayerFactory.Get(_personService.GetPlayerById(id));
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

        /// <inheritdoc />
        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            var dto = _personService.SavePlayer(PlayerFactory.Get(Item, Mode == ScreenMode.Creation ? CrudStatus.Created : CrudStatus.Updated));
            Item = PlayerFactory.Get(dto);
            return true;
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

        #endregion Properties Changed

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                //case "City":
                //    if (!string.IsNullOrEmpty(Item.Address.City) && string.IsNullOrEmpty(Item.Address.PostalCode))
                //    {
                //        Item.PostalCode = AllCities.Where(c => c.City == Item.City).Select(c => c.PostalCode).FirstOrDefault();
                //    }
                //    break;

                //case "PostalCode":
                //    if (!string.IsNullOrEmpty(Item.PostalCode) && string.IsNullOrEmpty(Item.City))
                //    {
                //        Item.City = AllCities.Where(c => c.PostalCode == Item.PostalCode).Select(c => c.City).FirstOrDefault();
                //    }
                //    break;

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

        #endregion Methods
    }
}