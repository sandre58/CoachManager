using System;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
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

        public DateTime? CreatedDate { get { return _createdDate; } set { SetProperty(ref _createdDate, value); } }

        public string CreatedBy { get { return _createdBy; } set { SetProperty(ref _createdBy, value); } }

        public DateTime? ModifiedDate { get { return _modifiedDate; } set { SetProperty(ref _modifiedDate, value); } }

        public string ModifiedBy { get { return _modifiedBy; } set { SetProperty(ref _modifiedBy, value); } }

        #endregion Members
    }
}