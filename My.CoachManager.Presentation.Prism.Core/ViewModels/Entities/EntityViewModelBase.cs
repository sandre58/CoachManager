using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Entities
{
    public abstract class EntityViewModelBase : ViewModelBase
    {
        #region Fields

        private DateTime? _createdDate;
        private string _createdBy;
        private DateTime? _modifiedDate;
        private string _modifiedBy;

        #endregion Fields

        #region Members

        /// <summary>
        /// Gets or sets the created date.
        /// </summary>
        public DateTime? CreatedDate { get { return _createdDate; } set { SetProperty(ref _createdDate, value); } }

        /// <summary>
        /// Gets or sets the created by.
        /// </summary>
        public string CreatedBy { get { return _createdBy; } set { SetProperty(ref _createdBy, value); } }

        /// <summary>
        /// Gets or sets the modified date.
        /// </summary>
        public DateTime? ModifiedDate { get { return _modifiedDate; } set { SetProperty(ref _modifiedDate, value); } }

        /// <summary>
        /// Gets or sets the modified by.
        /// </summary>
        public string ModifiedBy { get { return _modifiedBy; } set { SetProperty(ref _modifiedBy, value); } }

        #endregion Members
    }
}