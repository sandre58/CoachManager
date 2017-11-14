using System.Collections.Generic;
using System.Linq;
using My.CoachManager.Application.Dtos.Players;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Enums;
using My.CoachManager.Presentation.Resources.Strings.Screens;
using My.CoachManager.Presentation.ServiceAgent;
using My.CoachManager.Presentation.ServiceAgent.PlayerServiceReference;
using My.CoachManager.Presentation.ViewModels.Core;
using My.CoachManager.Presentation.ViewModels.Mapping;

namespace My.CoachManager.Presentation.ViewModels.Players
{
    public partial class EditPlayerViewModel : EditViewModel<PlayerViewModel>
    {
        #region Constants

        public const string DefaultCountry = "France";

        #endregion Constants

        #region Fields

        private IEnumerable<CategoryViewModel> _allCategories;
        private IEnumerable<CountryViewModel> _allCountries;
        private IEnumerable<CityViewModel> _allCities;
        private IEnumerable<string> _allCitiesName;
        private IEnumerable<string> _allPostalCodes;
        private IEnumerable<string> _allSizes;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="EditPlayerViewModel"/>.
        /// </summary>
        public EditPlayerViewModel()
        {
        }

        /// <summary>
        /// Initialise a new instance of <see cref="EditPlayerViewModel"/>.
        /// </summary>
        /// <param name="id"></param>
        public EditPlayerViewModel(int id)
            : base(id)
        {
        }

        #endregion Constructors

        #region Properties

        /// <summary>
        /// Get the display Name.
        /// </summary>
        public override string DisplayName
        {
            get
            {
                return Mode == ModeView.Modification ? PlayersResources.EditPlayer : PlayersResources.CreatePlayer;
            }
        }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public IEnumerable<CategoryViewModel> AllCategories
        {
            get { return _allCategories; }
            set
            {
                if (Equals(_allCategories, value)) return;
                _allCategories = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets countries.
        /// </summary>
        public IEnumerable<CountryViewModel> AllCountries
        {
            get { return _allCountries; }
            set
            {
                if (Equals(_allCountries, value)) return;
                _allCountries = value;
                NotifyOfPropertyChange();
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
                if (Equals(_allCities, value)) return;
                _allCities = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets cities.
        /// </summary>
        public IEnumerable<string> AllCitiesNames
        {
            get { return _allCitiesName; }
            set
            {
                if (Equals(_allCitiesName, value)) return;
                _allCitiesName = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets cities.
        /// </summary>
        public IEnumerable<string> AllPostalCodes
        {
            get { return _allPostalCodes; }
            set
            {
                if (Equals(_allPostalCodes, value)) return;
                _allPostalCodes = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets sizes.
        /// </summary>
        public IEnumerable<string> AllSizes
        {
            get { return _allSizes; }
            set
            {
                if (Equals(_allSizes, value)) return;
                _allSizes = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialise the window.
        /// </summary>
        protected override void Initialize()
        {
            base.Initialize();

            Item.PropertyChanged += OnCityChanged;

            InitializeContacts();
        }

        /// <summary>
        /// Load Data.
        /// </summary>
        /// <returns></returns>
        protected override void LoadDataCore()
        {
            using (var client = ServiceClientFactory.Create<PlayerServiceClient, IPlayerService>())
            {
                AllCategories = client.GetCategories().ToViewModels<CategoryViewModel>();
                AllCountries = client.GetCountries().ToViewModels<CountryViewModel>();
                AllCities = client.GetCities().Select(c => new CityViewModel()
                {
                    City = c.City,
                    PostalCode = c.PostalCode
                }).ToArray();

                AllCitiesNames = AllCities.Select(c => c.City).OrderBy(c => c);
                AllPostalCodes = AllCities.Select(c => c.PostalCode).OrderBy(c => c);

                if (Mode == ModeView.Creation)
                {
                    var country = AllCountries.FirstOrDefault(c => c.Label == DefaultCountry);

                    if (country != null) Item.CountryId = country.Id;
                }
                else
                {
                    Item = client.GetById(ItemId).ToViewModel<PlayerViewModel>();
                }
            }

            AllSizes = PlayerConstants.DefaultSizes;
            AllPhoneLabels = ContactConstants.DefaultPhoneLabels;
            AllEmailLabels = ContactConstants.DefaultEmailLabels;
        }

        /// <summary>
        /// Save item.
        /// </summary>
        protected override void SaveDataCore()
        {
            using (var client = ServiceClientFactory.Create<PlayerServiceClient, IPlayerService>())
            {
                Item = client.CreatePlayer(Item.ToDto<PlayerDto>()).ToViewModel<PlayerViewModel>();
            }
        }

        /// <summary>
        /// On property changed.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnCityChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
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