using System;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    public abstract class EntityViewModel : BaseViewModel
    {
        #region Fields

        private int _id;
        private DateTime? _createdDate;
        private string _createdBy;
        private DateTime? _modifiedDate;
        private string _modifiedBy;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Get the Id.
        /// </summary>
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                SetEntityProperty("Id", () => _id = value, value, Id);
            }
        }

        public DateTime? CreatedDate { get { return _createdDate; } set { SetEntityProperty(() => _createdDate = value, value, CreatedDate); } }

        public string CreatedBy { get { return _createdBy; } set { SetEntityProperty(() => _createdBy = value, value, CreatedBy); } }

        public DateTime? ModifiedDate { get { return _modifiedDate; } set { SetEntityProperty(() => _modifiedDate = value, value, ModifiedDate); } }

        public string ModifiedBy { get { return _modifiedBy; } set { SetEntityProperty(() => _modifiedBy = value, value, ModifiedBy); } }

        /// <summary>
        /// Get the business Key.
        /// </summary>
        public virtual object BusinessKey
        {
            get
            {
                return Id;
            }
        }

        #endregion Public Properties
    }
}