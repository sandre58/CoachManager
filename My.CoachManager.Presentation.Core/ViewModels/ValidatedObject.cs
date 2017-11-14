using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using My.CoachManager.Presentation.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Core.ViewModels
{
    /// <summary>
    /// Provides functionalities to validate an object.
    /// </summary>
    public class ValidatedObject : IValidatable
    {
        #region Constructors and Destructors

        /// <summary>
        /// Create a new instance of <see cref="ValidatedObject"/>.
        /// </summary>
        public ValidatedObject()
        {
            ValidationErrors = new Dictionary<string, IEnumerable<ValidationResult>>();
        }

        #endregion Constructors and Destructors

        #region Public Events

        /// <summary>
        /// Fire when errors changed.
        /// </summary>
        public virtual event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        /// Has errors ?
        /// </summary>
        public virtual bool HasErrors
        {
            get
            {
                return ValidationErrors.Any();
            }
        }

        #endregion Public Properties

        #region Properties

        protected virtual IDictionary<string, IEnumerable<ValidationResult>> ValidationErrors { get; private set; }

        #endregion Properties

        #region Public Methods and Operators

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

        #endregion Public Methods and Operators

        #region Methods

        /// <summary>
        /// Test if property is valid.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value.</param>
        /// <param name="raisePropertyErrorChanged"></param>
        /// <returns></returns>
        protected bool IsPropertyValid(string propertyName, object value, bool raisePropertyErrorChanged = true)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");

            var metadataAttrib = GetType().GetCustomAttributes(typeof(MetadataTypeAttribute), true).OfType<MetadataTypeAttribute>().FirstOrDefault();

            if (metadataAttrib != null)
            {
                TypeDescriptor.AddProviderTransparent(new AssociatedMetadataTypeTypeDescriptionProvider(GetType(), metadataAttrib.MetadataClassType), GetType());
            }

            var validationResults = new List<ValidationResult>();
            if (Validator.TryValidateProperty(value, new ValidationContext(this) { MemberName = propertyName }, validationResults))
            {
                if (ValidationErrors.ContainsKey(propertyName))
                {
                    ValidationErrors.Remove(propertyName);

                    if (raisePropertyErrorChanged) NotifyPropertyErrorChanged(propertyName);
                }

                return true;
            }

            if (ValidationErrors.ContainsKey(propertyName))
            {
                ValidationErrors[propertyName] = validationResults;
            }
            else
            {
                ValidationErrors.Add(propertyName, validationResults);

                if (raisePropertyErrorChanged) NotifyPropertyErrorChanged(propertyName);
            }

            return false;
        }

        /// <summary>
        /// Notify when error occured.
        /// </summary>
        /// <param name="propertyName">The property Name.</param>
        protected virtual void NotifyPropertyErrorChanged(string propertyName)
        {
            System.Diagnostics.Contracts.Contract.Requires<ArgumentNullException>(propertyName != null, "propertyName");

            if (ErrorsChanged != null)
            {
                ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
            }
        }

        /// <summary>
        /// Check if the entity is valid.
        /// </summary>
        /// <returns></returns>
        public bool IsValid(bool raisePropertyErrorChanged = true)
        {
            var isValid = true;

            var type = GetType();
            foreach (var property in type.GetProperties())
            {
                if (!IsPropertyValid(property.Name, property.GetValue(this), raisePropertyErrorChanged))
                {
                    isValid = false;
                }

                var collection = property.GetValue(this) as ICollection;

                if (collection != null)
                {
                    foreach (var validatable in collection.OfType<IValidatable>().Where(validatable => !validatable.IsValid(raisePropertyErrorChanged)))
                    {
                        isValid = false;
                    }
                }
            }

            return isValid;
        }

        #endregion Methods
    }
}