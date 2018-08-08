using System.Collections.Generic;
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
        /// Get or Set Add Phone Command.
        /// </summary>
        public DelegateCommand AddPhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Phone Command.
        /// </summary>
        public DelegateCommand<PhoneModel> RemovePhoneCommand { get; set; }

        /// <summary>
        /// Get or Set Add Email Command.
        /// </summary>
        public DelegateCommand AddEmailCommand { get; set; }

        /// <summary>
        /// Get or Set Remove Email Command.
        /// </summary>
        public DelegateCommand<EmailModel> RemoveEmailCommand { get; set; }

        #endregion Properties

        #region Methods

        /// <summary>
        /// Add a contact.
        /// </summary>
        public void AddPhone()
        {
            Item.Phones.Add(new PhoneModel
            {
                PersonId = Item.Id
            });
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        public void RemovePhone(PhoneModel phone)
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
            Item.Emails.Add(new EmailModel
            {
                PersonId = Item.Id
            });
        }

        /// <summary>
        /// Remove a contact.
        /// </summary>
        public void RemoveEmail(EmailModel email)
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