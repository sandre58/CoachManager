using My.CoachManager.Presentation.ViewModels.Core;

namespace My.CoachManager.Presentation.ViewModels.Players
{
    public class PlayerFiltersViewModel : FiltersViewModel
    {
        #region Fields

        private string _lastName;
        private string _firstName;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Get or set last name.
        /// </summary>
        public string LastName
        {
            get
            {
                return _lastName;
            }
            set
            {
                if (value == _lastName)
                {
                    return;
                }

                _lastName = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Get or set first name.
        /// </summary>
        public string FirstName
        {
            get
            {
                return _firstName;
            }
            set
            {
                if (value == _firstName)
                {
                    return;
                }

                _firstName = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties
    }
}