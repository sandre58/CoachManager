using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Metadatas;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    /// <summary>
    /// Provides properties for a Address Entity.
    /// </summary>
    [MetadataType(typeof(AddressMetadata))]
    public class AddressViewModel : EntityViewModel
    {
        private string _row1;

        /// <summary>
        /// Gets or sets the first row.
        /// </summary>
        public string Row1
        {
            get { return _row1; }
            set { SetProperty(ref _row1, value); }
        }

        private string _row2;

        /// <summary>
        /// Gets or sets the second row.
        /// </summary>
        public string Row2
        {
            get { return _row2; }
            set { SetProperty(ref _row2, value); }
        }

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

        private int? _countryId;

        /// <summary>
        /// Gets or sets the country id.
        /// </summary>
        public int? CountryId
        {
            get { return _countryId; }
            set { SetProperty(ref _countryId, value); }
        }

        private CountryViewModel _country;

        /// <summary>
        /// Gets or sets the country.
        /// </summary>
        public CountryViewModel Country
        {
            get { return _country; }
            set { SetProperty(ref _country, value); }
        }

        private double _latitude;

        /// <summary>
        /// Gets or sets the latitude.
        /// </summary>
        public double Latitude
        {
            get { return _latitude; }
            set { SetProperty(ref _latitude, value); }
        }

        private double _longitude;

        /// <summary>
        /// Gets or sets the longitude.
        /// </summary>
        public double Longitude
        {
            get { return _longitude; }
            set { SetProperty(ref _longitude, value); }
        }
    }
}