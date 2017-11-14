using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Caliburn.Micro;
using My.CoachManager.Presentation.Core.Arguments;
using My.CoachManager.Presentation.Core.ViewModels.Screens.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels.Screens
{
    /// <summary>
    /// ViewModel de base
    /// </summary>
    public abstract class ScreenViewModel : Screen, IDataErrorInfo, IScreenViewModel
    {
        #region Fields

        private ICollection<EntityViewModel> _trackedEntities;

        #endregion Fields

        #region Public Properties

        /// <summary>
        /// Check if screen is modified.
        /// </summary>
        public virtual bool IsModified
        {
            get
            {
                return TrackedEntities != null && TrackedEntities.Any(trackedEntity => trackedEntity.IsModified);
            }
        }

        /// <summary>
        /// Get or set the entities.
        /// </summary>
        public virtual ICollection<EntityViewModel> TrackedEntities
        {
            get
            {
                return _trackedEntities;
            }

            set
            {
                if (Equals(value, _trackedEntities))
                {
                    return;
                }

                _trackedEntities = value;
                NotifyOfPropertyChange(() => TrackedEntities);
            }
        }

        #endregion Public Properties

        #region Cnstructors

        /// <summary>
        /// Initialise a new instance.
        /// </summary>
        public ScreenViewModel()
        {
            InternalInitialize();
        }

        #endregion Cnstructors

        #region Methods

        public override void TryClose(bool? dialogResult = null)
        {
            base.TryClose(dialogResult);
            var val = dialogResult ?? false;
            OnCloseView(val);
        }

        /// <summary>
        /// Initialise properties.
        /// </summary>
        private void InternalInitialize()
        {
            Initialize();
        }

        /// <summary>
        /// Initialise properties.
        /// </summary>
        protected virtual void Initialize()
        {
        }

        #endregion Methods

        #region IDataErrorInfo Members

        /// <summary>
        /// Returns error against an object.
        /// </summary>
        public string Error
        {
            get { return null; }
        }

        /// <summary>
        /// Gets error against a property name.
        /// </summary>
        /// <param name="columnName"></param>
        /// <returns></returns>
        public string this[string columnName]
        {
            get { return GetErrorForProperty(columnName); }
        }

        #region Protected Members

        /// <summary>
        /// A virtual overridable method for returning an error against a property value.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        protected virtual string GetErrorForProperty(string propertyName)
        {
            return null;
        }

        #endregion Protected Members

        #endregion IDataErrorInfo Members

        #region Event

        public event EventHandler<CloseViewEventArgs> CloseView;

        /// <summary>
        /// Call the event CloseView.
        /// </summary>
        protected virtual void OnCloseView(bool dialogResult)
        {
            var handler = CloseView;
            if (handler != null) handler(this, new CloseViewEventArgs(dialogResult));
        }

        #endregion Event
    }
}