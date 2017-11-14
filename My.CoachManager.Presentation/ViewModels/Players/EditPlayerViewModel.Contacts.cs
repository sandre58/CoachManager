using System.Collections.Generic;

namespace My.CoachManager.Presentation.ViewModels.Players
{
    public partial class EditPlayerViewModel
    {
        #region Fields

        private IEnumerable<string> _allPhoneLabels;
        private IEnumerable<string> _allEmailLabels;

        #endregion Fields

        #region Properties

        /// <summary>
        /// Gets or sets phone labels.
        /// </summary>
        public IEnumerable<string> AllPhoneLabels
        {
            get { return _allPhoneLabels; }
            set
            {
                if (Equals(_allPhoneLabels, value)) return;
                _allPhoneLabels = value;
                NotifyOfPropertyChange();
            }
        }

        /// <summary>
        /// Gets or sets email labels.
        /// </summary>
        public IEnumerable<string> AllEmailLabels
        {
            get { return _allEmailLabels; }
            set
            {
                if (Equals(_allEmailLabels, value)) return;
                _allEmailLabels = value;
                NotifyOfPropertyChange();
            }
        }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Initialise contacts.
        /// </summary>
        private void InitializeContacts()
        {
            AddPhone();
            AddEmail();
        }

        /// <summary>
        /// Add a contact.
        /// </summary>
        public void AddPhone()
        {
            Item.Phones.Add();
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        public void RemovePhone(PhoneViewModel phone)
        {
            if (Item.Phones.Count > 1)
                Item.Phones.Remove(phone);
            else if (Item.Phones.Count == 1)
            {
                phone.Label = "";
                phone.Value = "";
            }
        }

        /// <summary>
        /// Add a contact.
        /// </summary>
        public void AddEmail()
        {
            Item.Emails.Add();
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        public void RemoveEmail(EmailViewModel email)
        {
            if (email != null)
            {
                if (Item.Emails.Count > 1)
                    Item.Emails.Remove(email);
                else if (Item.Phones.Count == 1)
                {
                    email.Label = "";
                    email.Value = "";
                }
            }
        }

        #endregion Methods
    }
}