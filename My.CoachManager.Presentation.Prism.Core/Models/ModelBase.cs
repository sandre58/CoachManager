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
        /// Get errors.
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<string> GetErrors()
        {
            var type = GetType();
            var validationErrors = ValidationErrors.SelectMany(x => x.Value).Select(error => error.ErrorMessage).ToList();
            var complexValidationErrors =
                type.GetProperties().Select(x => x.GetValue(this)).OfType<IValidatable>().ToList().SelectMany(x => x.GetErrors()).ToList();
            var collectionValidationErrors =
                type.GetProperties().Select(x => x.GetValue(this)).OfType<ICollection>().ToList().SelectMany(x => x.OfType<IValidatable>()).SelectMany(x => x.GetErrors()).ToList();
            return validationErrors.Concat(complexValidationErrors).Concat(collectionValidationErrors);
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
        /// Gets error for a property.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        protected virtual IEnumerable<string> ComputeErrors(string propertyName, object value)
        {
            return new List<string>();
        }

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

            // Validation by Metadata
            Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
            // Custom Validation
            validationResults.AddRange(ComputeErrors(propertyName, value).Select(x => new ValidationResult(x)));

            // Is Valid
            if (!validationResults.Any())
            {
                if (!ValidationErrors.ContainsKey(propertyName)) return;
                ValidationErrors.Remove(propertyName);
                NotifyErrorsChanged(propertyName);
                return;
            }

            // Is not valid
            if (ValidationErrors.ContainsKey(propertyName))
            {
                ValidationErrors[propertyName] = validationResults;
            }
            else
            {
                ValidationErrors.Add(propertyName, validationResults);
            }

            NotifyErrorsChanged(propertyName);
        }

        /// <inheritdoc />
        /// <summary>
        /// Check if the entity is valid.
        /// </summary>
        /// <returns></returns>
        public bool Validate()
        {
            var type = GetType();
            var result = true;
            foreach (var property in type.GetProperties())
            {
                // All Property
                ValidateProperty(property.Name, property.GetValue(this));
                result = result && !HasErrors;

                // Complex property
                if(property.GetValue(this) is IValidatable entity)
                result = result && entity.Validate();

                // Collection property
                if (property.GetValue(this) is ICollection collection)
                {
                    result = result && collection.OfType<IValidatable>().All(validatable => validatable.Validate());
                }
            }

            return result;
        }

        #endregion IValidatable

        #region IPropertyChanged

        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        /// <param name="before">The before value.</param>
        /// <param name="after">The after value.</param>
        protected virtual void OnPropertyChanged(string propertyName, object before, object after)
        {
            var prop = GetType().GetProperty(propertyName);
            if (prop != null)
            {
                // The property exists
                var isPublic = (prop.GetGetMethod(true) ?? prop.GetSetMethod(true)).IsPublic;
                if (isPublic)
                {
                    ValidateProperty(propertyName, after);
                }
            }

            RaisePropertyChanged(propertyName);
        }

        #endregion IPropertyChanged
    }
}