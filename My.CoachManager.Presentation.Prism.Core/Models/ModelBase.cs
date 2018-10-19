using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reactive.Linq;
using System.Reflection;
using Microsoft.Practices.ObjectBuilder2;
using My.CoachManager.Presentation.Prism.Core.Attributes.Validation;
using My.CoachManager.Presentation.Prism.Core.Rules;
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
        private IDictionary<string, List<ValidationResult>> ValidationErrors { get; }

        /// <summary>
        /// Gets the rules which provide the errors.
        /// </summary>
        /// <value>The rules this instance must satisfy.</value>
        protected RuleCollection Rules { get; } = new RuleCollection();

        /// <summary>
        /// Gets the when errors changed observable event. Occurs when the validation errors have changed for a property or for the entire object.
        /// </summary>
        /// <value>
        /// The when errors changed observable event.
        /// </value>
        public IObservable<string> WhenErrorsChanged
        {
            get
            {
                return Observable
                    .FromEventPattern<DataErrorsChangedEventArgs>(
                        h => ErrorsChanged += h,
                        h => ErrorsChanged -= h)
                    .Select(x => x.EventArgs.PropertyName);
            }
        }

        #endregion Members

        #region Constructors

        protected ModelBase()
        {
            ValidationErrors = new Dictionary<string, List<ValidationResult>>();

            this.PropertyChanged += ModelBase_PropertyChanged;
        }

        private void ModelBase_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
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
        private event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        /// <inheritdoc />
        /// <summary>
        /// Occurs when the validation errors have changed for a property or for the entire object.
        /// </summary>
        event EventHandler<DataErrorsChangedEventArgs> INotifyDataErrorInfo.ErrorsChanged
        {
            add => ErrorsChanged += value;
            remove => ErrorsChanged -= value;
        }

        /// <summary>
        /// Gets a value indicating whether the object has validation errors.
        /// </summary>
        /// <value><c>true</c> if this instance has errors, otherwise <c>false</c>.</value>
        public virtual bool HasErrors => ValidationErrors.Any();

        /// <summary>
        /// Gets the validation errors for a specified property or for the entire object.
        /// </summary>
        /// <param name="propertyName">Name of the property to retrieve errors for. <c>null</c> to
        /// retrieve all errors for this instance.</param>
        /// <returns>A collection of errors.</returns>
        public IEnumerable GetErrors(string propertyName)
        {
            IEnumerable result;
            if (string.IsNullOrEmpty(propertyName))
            {
                var allErrors = new List<string>();

                foreach (var keyValuePair in ValidationErrors)
                {
                    allErrors.AddRange(keyValuePair.Value.Select(x => x.ErrorMessage));
                }

                result = allErrors;
            }
            else
            {
                result = ValidationErrors.ContainsKey(propertyName) ? ValidationErrors[propertyName].Select(x => x.ErrorMessage) : new List<string>();
            }

            return result;
        }

        /// <summary>
        /// Gets the validation errors for the entire object.
        /// </summary>
        /// <returns>A collection of errors.</returns>
        public virtual IEnumerable<string> GetErrors()
        {
            var result = new List<string>();
            result.AddRange((List<string>)GetErrors(null));

            var type = GetType();
            var complexValidationErrors =
                type.GetProperties().Select(x => x.GetValue(this)).OfType<IValidatable>().ToList().SelectMany(x => x.GetErrors()).ToList();
            result.AddRange(complexValidationErrors);

            var collectionValidationErrors =
                type.GetProperties().Select(x => x.GetValue(this)).OfType<ICollection>().ToList().SelectMany(x => x.OfType<IValidatable>()).SelectMany(x => x.GetErrors()).ToList();
            result.AddRange(collectionValidationErrors);

            return result;
        }

        /// <summary>
        /// Notifies when errors changed.
        /// </summary>
        /// <param name="propertyName"></param>
        protected void OnErrorsChanged(string propertyName)
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
        protected void ValidateProperty(string propertyName, object value)
        {
            var validationResults = new List<ValidationResult>();

            TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(GetType()), GetType());

            // Validation by Metadata
            Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults);
            // Custom Validation
            validationResults.AddRange(Rules.Apply(this, propertyName).Select(x => new ValidationResult(x.ToString())).ToList());

            // Is Valid
            if (!validationResults.Any())
            {
                if (!ValidationErrors.ContainsKey(propertyName)) return;
                ValidationErrors.Remove(propertyName);
                OnErrorsChanged(propertyName);
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

            OnErrorsChanged(propertyName);
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
                if (property.GetValue(this) is IValidatable entity)
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

                    // Validate other property defined by [ValidateProperty(<PropertyName>)] attributes.
                    prop.GetCustomAttributes().OfType<ValidatePropertyAttribute>().ForEach(x =>
                    {
                        var property = GetType().GetProperty(x.PropertyName);
                        var value = property?.GetValue(this);
                        ValidateProperty(x.PropertyName, value);
                    });
                }
            }

            RaisePropertyChanged(propertyName);
        }

        #endregion IPropertyChanged
    }
}