using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.ViewModels
{
    public class CityViewModel : ViewModelBase
    {
        private string _postalCode;
        public string PostalCode { get { return _postalCode; } set { SetProperty(ref _postalCode, value); } }

        private string _city;
        public string City { get { return _city; } set { SetProperty(ref _city, value); } }
    }
}