using System.Collections.Generic;
using My.CoachManager.CrossCutting.Logging;
using My.CoachManager.Presentation.Prism.Core.Services;
using My.CoachManager.Presentation.Prism.Core.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels;
using My.CoachManager.Presentation.Prism.ViewModels.Mapping;
using System.Linq;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Presentation.Prism.Administration.Resources.Strings;
using My.CoachManager.Presentation.ServiceAgent.AdminServiceReference;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Administration.ViewModels
{
    public partial class PlayerEditViewModel : EditViewModel<PlayerViewModel>, IPlayerEditViewModel
    {
        #region Constants

        public const string DefaultCountry = "France";

        #endregion Constants

        #region Fields

        private readonly IAdminService _adminService;
        private IEnumerable<CategoryViewModel> _allCategories;
        private IEnumerable<CountryViewModel> _allCountries;
        private IEnumerable<CityViewModel> _allCities;
        private IEnumerable<string> _allCitiesName;
        private IEnumerable<string> _allPostalCodes;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="CategoryEditViewModel"/>.
        /// </summary>
        public PlayerEditViewModel(IAdminService adminService, IDialogService dialogService, ILogger logger)
            : base(dialogService, logger)
        {
            _adminService = adminService;
            Title = AdministrationResources.PlayerTitle;

            AddEmailCommand = new DelegateCommand(AddEmail);
            RemoveEmailCommand = new DelegateCommand<EmailViewModel>(RemoveEmail);
            AddPhoneCommand = new DelegateCommand(AddPhone);
            RemovePhoneCommand = new DelegateCommand<PhoneViewModel>(RemovePhone);
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

        #endregion Members

        #region Methods

        protected override void BeforeSave()
        {
            base.BeforeSave();

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
        }

        /// <summary>
        /// Save.
        /// </summary>
        protected override void SaveItemCore()
        {
            Item = _adminService.CreateOrUpdatePlayer(Item.ToDto<PlayerDto>()).ToViewModel<PlayerViewModel>();
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        protected override void LoadDataCore(bool isFirstLoading = false)
        {
            if (isFirstLoading)
            {
                AllCategories = _adminService.GetCategoriesForPlayer().ToViewModels<CategoryViewModel>();
                AllCountries = _adminService.GetCountriesForPlayer().ToViewModels<CountryViewModel>();
                AllCities = _adminService.GetCitiesForPlayer().Select(c => new CityViewModel()
                {
                    City = c.City,
                    PostalCode = c.PostalCode
                }).ToArray();
                AllCitiesNames = AllCities.Select(c => c.City).OrderBy(c => c);
                AllPostalCodes = AllCities.Select(c => c.PostalCode).OrderBy(c => c);
                AllPhoneLabels = ContactConstants.DefaultPhoneLabels;
                AllEmailLabels = ContactConstants.DefaultEmailLabels;
            }

            base.LoadDataCore(isFirstLoading);
        }

        /// <summary>
        /// Call after load data.
        /// </summary>
        protected override void AfterLoadData(bool isFirstLoading = false)
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

            base.AfterLoadData(isFirstLoading);
        }

        /// <summary>
        /// Load an item from data source.
        /// </summary>
        /// <param name="id"></param>
        protected override PlayerViewModel LoadItemCore(int id)
        {
            var item = _adminService.GetPlayerById(id);
            return item.ToViewModel<PlayerViewModel>();
        }

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnItemPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "City":
                    if (!string.IsNullOrEmpty(Item.City) && string.IsNullOrEmpty(Item.PostalCode))
                    {
                        Item.PostalCode = AllCities.Where(c => c.City == Item.City).Select(c => c.PostalCode).FirstOrDefault();
                    }
                    break;

                case "PostalCode":
                    if (!string.IsNullOrEmpty(Item.PostalCode) && string.IsNullOrEmpty(Item.City))
                    {
                        Item.City = AllCities.Where(c => c.PostalCode == Item.PostalCode).Select(c => c.City).FirstOrDefault();
                    }
                    break;
            }
        }

        #endregion Methods
    }
}