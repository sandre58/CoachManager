using My.CoachManager.Presentation.Core.ViewModels;

namespace My.CoachManager.Presentation.ViewModels
{
    public class CityViewModel : BaseViewModel
    {
        private string _postalCode;
        public string PostalCode { get { return _postalCode; } set { SetEntityProperty(() => _postalCode = value, value, PostalCode); } }

        private string _city;
        public string City { get { return _city; } set { SetEntityProperty(() => _city = value, value, City); } }
    }
}