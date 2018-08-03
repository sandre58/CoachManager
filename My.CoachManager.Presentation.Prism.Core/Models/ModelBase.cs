using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Practices.ObjectBuilder2;
using Prism.Mvvm;
using PropertyChanged;

namespace My.CoachManager.Presentation.Prism.Core.Models
{
    /// <inheritdoc cref="BindableBase" />
    /// <summary>
    /// The model base.
    /// </summary>
    /// <seealso cref="T:System.ComponentModel.INotifyPropertyChanged" />
    [AddINotifyPropertyChangedInterface]
    public abstract class ModelBase : BindableBase, INotifyDataErrorInfo, IValidatable
    {
        #region Members

        /// <summary>
        /// Gets the validation errors.
        /// </summary>
        protected IDictionary<string, IEnumerable<ValidationResult>> ValidationErrors { get; }

        #endregion Members

        #region Constructors

        protected ModelBase()
        {
            ValidationErrors = new Dictionary<string, IEnumerable<ValidationResult>>();
        }

        #endregion Constructors

        #region Methods

        /// <summary>
        /// Raises this object's PropertyChanged event.
        /// </summary>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            RaisePropertyChanged(propertyName);
        }

        #endregion Methods

        #region INotifyDataErrorInfo

        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire object.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <summary>
        /// Has errors ?
        /// </summary>
        public virtual bool HasErrors => ValidationErrors.Any();

        /// <inheritdoc />
        /// <summary>
        /// Get errors.
        /// </summary>
        /// <param name="propertyName">Property Name.</param>
        /// <returns></returns>
        public virtual IEnumerable GetErrors(string propertyName)
        {
            return (propertyName != null) && ValidationErrors.TryGetValue(propertyName, out var validationResults)
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

        #endregion INotifyDataErrorInfo

        #region IValidatable

        /// <summary>
        /// Test if property is valid.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value.</param>
        /// <returns></returns>
        private void ValidateProperty(string propertyName, object value)
        {
            var validationResults = new List<ValidationResult>();

            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(GetType()), GetType());

            if (Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults))
            {
                if (!ValidationErrors.ContainsKey(propertyName)) return;
                ValidationErrors.Remove(propertyName);
                NotifyErrorsChanged(propertyName);
                return;
            }

            if (ValidationErrors.ContainsKey(propertyName))
            {
                ValidationErrors[propertyName] = validationResults;
                NotifyErrorsChanged(propertyName);
            }
            else
            {
                ValidationErrors.Add(propertyName, validationResults);
                NotifyErrorsChanged(propertyName);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if the entity is valid.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            var type = GetType();
            bool? result = true;
            foreach (var property in type.GetProperties())
            {
                ValidateProperty(property.Name, property.GetValue(this));

                var collection = property.GetValue(this) as ICollection;

                collection?.OfType<IValidatable>().ForEach(validatable => validatable.Validate());

                result = collection?.OfType<IValidatable>().All(validatable => validatable.Validate());
            }

            return !HasErrors && (result == null || (bool)result);
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
            ValidateProperty(propertyName, after);

            RaisePropertyChanged(propertyName);
        }

        #endregion IPropertyChanged
    }
}