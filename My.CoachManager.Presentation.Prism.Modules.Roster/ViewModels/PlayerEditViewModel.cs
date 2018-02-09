using System.Collections.Generic;
using System.IO;
using System.Linq;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Presentation.Prism.Core;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Screens;
using My.CoachManager.Presentation.Prism.Modules.Roster.Core.Resources.Strings;
using My.CoachManager.Presentation.Prism.Resources.Strings;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using My.CoachManager.Presentation.ServiceAgent.CategoryServiceReference;
using My.CoachManager.Presentation.ServiceAgent.PersonServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.ViewModels
{
    public partial class PlayerEditViewModel : EditViewModel<PlayerViewModel>, IPlayerEditViewModel
    {
        #region Constants

        public const string DefaultCountry = "France";

        #endregion Constants

        #region Fields

        private readonly IPersonService _personService;
        private readonly ICategoryService _categoryService;
        private IEnumerable<CategoryViewModel> _allCategories;
        private IEnumerable<CountryViewModel> _allCountries;
        private IEnumerable<CityViewModel> _allCities;
        private IEnumerable<string> _allCitiesName;
        private IEnumerable<string> _allPostalCodes;

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

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerEditViewModel"/>.
        /// </summary>
        public PlayerEditViewModel()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets categories.
        /// </summary>
        public IEnumerable<CategoryViewModel> AllCategories
        {
            get { return _allCategories; }
            set { SetProperty(ref _allCategories, value); }
        }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public IEnumerable<CountryViewModel> AllCountries
        {
            get { return _allCountries; }
            set
            {
                SetProperty(ref _allCountries, value);
            }
        }

        /// <summary>
        /// Gets or sets cities.
        /// </summary>
        public IEnumerable<CityViewModel> AllCities
        {
            get { return _allCities; }
            set
            {
                SetProperty(ref _allCities, value);
            }
        }

        /// <summary>
        /// Gets or sets cities names.
        /// </summary>
        public IEnumerable<string> AllCitiesNames
        {
            get { return _allCitiesName; }
            set
            {
                SetProperty(ref _allCitiesName, value);
            }
        }

        /// <summary>
        /// Gets or sets postal codes.
        /// </summary>
        public IEnumerable<string> AllPostalCodes
        {
            get { return _allPostalCodes; }
            set
            {
                SetProperty(ref _allPostalCodes, value);
            }
        }

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

            Title = PlayerResources.PlayerTitle;
        }

        /// <summary>
        /// Initializes commands.
        /// </summary>
        protected override void InitializeCommands()
        {
            base.InitializeCommands();

            AddEmailCommand = new DelegateCommand(AddEmail);
            RemoveEmailCommand = new DelegateCommand<EmailViewModel>(RemoveEmail);
            AddPhoneCommand = new DelegateCommand(AddPhone);
            RemovePhoneCommand = new DelegateCommand<PhoneViewModel>(RemovePhone);
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
            var filename = Locator.DialogService.ShowOpenFileDialog(ControlResources.AllImages);

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
            AllCategories = _categoryService.GetLabels().ToViewModels<CategoryViewModel>();
            AllCountries = _personService.GetCountries().ToViewModels<CountryViewModel>();

            // TODO : Address
            AllCities = new List<CityViewModel>();
            AllCitiesNames = AllCities.Select(c => c.City).OrderBy(c => c);
            AllPostalCodes = AllCities.Select(c => c.PostalCode).OrderBy(c => c);
        }

        /// <summary>
        /// Called after load data.
        /// </summary>
        protected override void OnLoadDataCompleted()
        {
            if (Mode == ScreenMode.Creation)
            {
                if (AllCountries != null)
                {
                    var country = AllCountries.FirstOrDefault(c => c.Label == DefaultCountry);

                    if (country != null) Item.CountryId = country.Id;
                }
            }

            if (Item != null)
            {
                Item.PropertyChanged += OnItemPropertyChanged;

                if (Item.Emails.Count == 0) AddEmail();
                if (Item.Phones.Count == 0) AddPhone();
            }

            base.OnLoadDataCompleted();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerViewModel LoadItemCore(int id)
        {
            var item = _personService.GetPlayer(id);
            return item.ToViewModel<PlayerViewModel>();
        }

        /// <summary>
        /// Called before save.
        /// </summary>
        protected override void OnSaveRequested()
        {
            var emptyEmailIndexes = Item.Emails.Where(x => string.IsNullOrEmpty(x.Value)).Select(x => Item.Emails.IndexOf(x)).OrderByDescending(x => x);

            foreach (var index in emptyEmailIndexes)
            {
                Item.Emails.RemoveAt(index);
            }

            var emptyPhoneIndexes = Item.Phones.Where(x => string.IsNullOrEmpty(x.Value)).Select(x => Item.Phones.IndexOf(x)).OrderByDescending(x => x);

            foreach (var index in emptyPhoneIndexes)
            {
                Item.Phones.RemoveAt(index);
            }

            base.OnSaveRequested();
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected override bool SaveItemCore()
        {
            Item = _personService.CreateOrUpdate(Item.ToDto<PlayerDto>()).ToViewModel<PlayerViewModel>();
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

            if (RemovePhotoCommand != null) RemovePhotoCommand.RaiseCanExecuteChanged();
            if (SelectPhotoCommand != null) SelectPhotoCommand.RaiseCanExecuteChanged();
        }

        #endregion Properties Changed

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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
                        Item.CategoryId = category != null ? category.Id : 0;
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