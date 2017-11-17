using System.Collections.Generic;
using System.Windows.Input;
using My.CoachManager.Presentation.Prism.ViewModels;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Administration.ViewModels
{
    public partial class PlayerEditViewModel
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
            set { SetProperty(ref _allPhoneLabels, value); }
        }

        /// <summary>
        /// Gets or sets email labels.
        /// </summary>
        public IEnumerable<string> AllEmailLabels
        {
            get { return _allEmailLabels; }
            set
            {
                SetProperty(ref _allEmailLabels, value);
            }
        }

        /// <summary>
        /// Get or Set Add Phone Command.
        /// </summary>
        public DelegateCommand AddPhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Phone Command.
        /// </summary>
        public DelegateCommand<PhoneViewModel> RemovePhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Add Email Command.
        /// </summary>
        public DelegateCommand AddEmailCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Email Command.
        /// </summary>
        public DelegateCommand<EmailViewModel> RemoveEmailCommand { get; set; }

        #endregion Properties

        #region Methods

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