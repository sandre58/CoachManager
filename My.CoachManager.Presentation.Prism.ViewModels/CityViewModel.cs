using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a City item.
    /// </summary>
    public class CityViewModel : ViewModelBase
    {
        private string _postalCode;

        /// <summary>
        /// Gets or sets the postal code.
        /// </summary>
        public string PostalCode
        {
            get { return _postalCode; }
            set { SetProperty(ref _postalCode, value); }
        }

        private string _city;

        /// <summary>
        /// Gets or sets the city.
        /// </summary>
        public string City
        {
            get { return _city; }
            set { SetProperty(ref _city, value); }
        }
    }
}