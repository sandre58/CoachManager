using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Prism.Mvvm;
using PropertyChanged;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <summary>
    /// The model base.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged"/>
    [AddINotifyPropertyChangedInterface]
    public abstract class ModelBase : BindableBase, INotifyDataErrorInfo
    {
        #region Members

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        protected IDictionary<string, IEnumerable<ValidationResult>> ValidationErrors { get; }

        #endregion

        #region Constructors

        protected ModelBase()
        {
            ValidationErrors = new Dictionary<string, IEnumerable<ValidationResult>>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            RaisePropertyChanged(propertyName);
        }

        #endregion

        #region Validation

        /// <summary>
        /// Test if property is valid.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private void ValidateProperty(string propertyName, object value)
        {
            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults))
            {
                if (ValidationErrors.ContainsKey(propertyName))
                {
                    NotifyErrorsChanged(propertyName);
                    ValidationErrors.Remove(propertyName);
                }
                return;
            }

            if (ValidationErrors.ContainsKey(propertyName))
            {
                NotifyErrorsChanged(propertyName);
                ValidationErrors[propertyName] = validationResults;
            }
            else
            {
                NotifyErrorsChanged(propertyName);
                ValidationErrors.Add(propertyName, validationResults);
            }
        }

        #endregion

        #region INotifyDataErrorInfo

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire object. 
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Has errors ?
        /// </summary>
        public virtual bool HasErrors => ValidationErrors.Any();

        /// <summary>
        /// Get errors.
        /// </summary>
        /// <param name="propertyName">Property Name.</param>
        /// <returns></returns>
        public virtual IEnumerable GetErrors(string propertyName)
        {
            IEnumerable<ValidationResult> validationResults;
            return (propertyName != null) && ValidationErrors.TryGetValue(propertyName, out validationResults)
                ? validationResults.Select(validationResult => validationResult.ErrorMessage)
                : Enumerable.Empty<object>();
        }
        
        /// <summary>
        /// Notifies when errors changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void NotifyErrorsChanged(string propertyName)
        {
            // Notify
            ErrorsChanged?.Invoke(this, new DataErrorsChangedEventArgs(propertyName));
        }
        #endregion

        #region IPropertyChanged

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="before">The before value.</param>
        /// <param name="after">The after value.</param>
        protected virtual void OnPropertyChanged(string propertyName, object before, object after)
        {
            // We check if the property is valid, which notifies if the property is in error.
            ValidateProperty(propertyName, before);
            RaisePropertyChanged(propertyName);
        }

        #endregion
    }
}
