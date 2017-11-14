using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    /// <summary>
    /// Provide functionalities to manage a view model in presentation.
    /// </summary>
    public abstract class BaseViewModel :
         ValidatedObject,
         IViewModel
    {
        #region Constants

        public const string ModifiedPropertyName = "IsModified";

        #endregion Constants

        #region Constructors and Destructors

        /// <summary>
        /// Provide a new instance of <see cref="BaseViewModel"/>.
        /// </summary>
        public BaseViewModel()
        {
            InitialPropertyValues = new Dictionary<string, object>();
            InternalInitialise();
        }

        #endregion Constructors and Destructors

        #region Public Events

        public virtual event PropertyChangedEventHandler PropertyChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Indicate if the entity is modified.
        /// </summary>
        public virtual bool IsModified
        {
            get
            {
                return InitialPropertyValues.Any();
            }
        }

        #endregion Public Properties

        #region Properties

        /// <summary>
        /// The initiale properties.
        /// </summary>
        protected virtual IDictionary<string, object> InitialPropertyValues { get; private set; }

        #endregion Properties

        #region Public Methods and Operators

        /// <summary>
        /// Initilise properties.
        /// </summary>
        protected virtual void Initialise()
        {
        }

        private void InternalInitialise()
        {
            Initialise();
        }

        /// <summary>
        /// Check if the property is modified.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <returns></returns>
        public virtual bool CheckIfPropertyModified(string propertyName)
        {
            return InitialPropertyValues.ContainsKey(propertyName);
        }

        /// <summary>
        /// Get the modified properties.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetModifiedProperties()
        {
            return InitialPropertyValues.Keys;
        }

        /// <summary>
        ///
        /// </summary>
        public virtual void ResetModified()
        {
            var oldModified = IsModified;

            InitialPropertyValues.Clear();

            if (IsModified != oldModified)
            {
                NotifyPropertyChanged(ModifiedPropertyName);
            }
        }

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>
        /// Notigy when a property change.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");

            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Set a property.
        /// </summary>
        /// <param name="propertyName">The property Name.</param>
        /// <param name="propertySetter">The action when property change.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        public void SetEntityProperty(Action propertySetter, object newValue, object oldValue, [CallerMemberName] string propertyName = "")
        {
            SetEntityProperty(propertyName, propertySetter, newValue, oldValue);
        }

        /// <summary>
        /// Set a property.
        /// </summary>
        /// <param name="propertyName">The property Name.</param>
        /// <param name="propertySetter">The action when property change.</param>
        /// <param name="newValue">The new value.</param>
        /// <param name="oldValue">The old value.</param>
        protected void SetEntityProperty(string propertyName, Action propertySetter, object newValue, object oldValue)
        {
            // Is the property changing ?
            if (Equals(newValue, oldValue))
            { // No.
                return;
            }

            object initalValue;
            var oldModified = IsModified;

            // Has the property already changed from initial value ?
            if (InitialPropertyValues.TryGetValue(propertyName, out initalValue))
            { // Yes.
                // Do we revert to the initial value ?
                if (Equals(newValue, initalValue))
                {
                    // Yes, so the remove it.
                    InitialPropertyValues.Remove(propertyName);
                }
            }
            else
            { // No, so we save the initial value.
                InitialPropertyValues.Add(propertyName, oldValue);
            }

            // We check if the property is valid, which notifies if the property is in error.
            IsPropertyValid(propertyName, newValue);

            propertySetter();

            NotifyPropertyChanged(propertyName);

            if (IsModified != oldModified)
            {
                NotifyPropertyChanged(ModifiedPropertyName);
            }
        }

        #endregion Methods
    }
}