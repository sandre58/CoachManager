using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Collections;
using My.CoachManager.Presentation.Prism.Models;
using Prism.Commands;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.ViewModels
{
    public partial class PlayerEditViewModel
    {
        #region Properties

        /// <summary>
        /// Gets or sets phone labels.
        /// </summary>
        public IEnumerable<string> AllPhoneLabels { get; set; }

        /// <summary>
        /// Gets or sets email labels.
        /// </summary>
        public IEnumerable<string> AllEmailLabels { get; set; }

        /// <summary>
        /// Gets phones.
        /// </summary>
        public ObservableItemsCollection<PhoneModel> Phones { get; private set; }

        /// <summary>
        /// Gets phones.
        /// </summary>
        public ObservableItemsCollection<EmailModel> Emails { get; private set; }

        /// <summary>
        /// Get or Set Add Phone Command.
        /// </summary>
        public DelegateCommand<PhoneModel> AddPhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Phone Command.
        /// </summary>
        public DelegateCommand<PhoneModel> RemovePhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Add Email Command.
        /// </summary>
        public DelegateCommand<EmailModel> AddEmailCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Email Command.
        /// </summary>
        public DelegateCommand<EmailModel> RemoveEmailCommand { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Add a contact.
        /// </summary>
        private void AddPhone(PhoneModel phone)
        {
            Phones.Add(new PhoneModel());
        }

        /// <summary>
        /// Can Add a contact.
        /// </summary>
        private bool CanAddPhone(PhoneModel phone)
        {
            return Phones.IndexOf(phone) == Phones.Count - 1 && !string.IsNullOrEmpty(phone.Value);
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        private void RemovePhone(PhoneModel phone)
        {
            if (Phones.Count > 1)
                Phones.Remove(phone);
            else if (Phones.Count == 1)
            {
                phone.Label = "";
                phone.Value = "";
            }
        }

        /// <summary>
        /// Can remove a contact.
        /// </summary>
        private bool CanRemovePhone(PhoneModel phone)
        {
            return Phones.Count > 1 || !string.IsNullOrEmpty(phone.Value);
        }

        /// <summary>
        /// Add a contact.
        /// </summary>
        private void AddEmail(EmailModel mail)
        {
            Emails.Add(new EmailModel());
        }

        /// <summary>
        /// Can Add a contact.
        /// </summary>
        private bool CanAddEmail(EmailModel mail)
        {
            return Emails.IndexOf(mail) == Emails.Count - 1 && !string.IsNullOrEmpty(mail.Value);
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        private void RemoveEmail(EmailModel email)
        {
            if (email != null)
            {
                if (Emails.Count > 1)
                    Emails.Remove(email);
                else if (Phones.Count == 1)
                {
                    email.Label = "";
                    email.Value = "";
                }
            }
        }

        /// <summary>
        /// Can remove a contact.
        /// </summary>
        private bool CanRemoveEmail(EmailModel mail)
        {
            return Emails.Count > 1 || !string.IsNullOrEmpty(mail.Value);
        }

        private void Contacts_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            UpdateContactsCommand();
        }

        private void UpdateContactsCommand()
        {
            RemoveEmailCommand?.RaiseCanExecuteChanged();
            RemovePhoneCommand?.RaiseCanExecuteChanged();
            AddEmailCommand?.RaiseCanExecuteChanged();
            AddPhoneCommand?.RaiseCanExecuteChanged();
        }

        #endregion Methods
    }
}