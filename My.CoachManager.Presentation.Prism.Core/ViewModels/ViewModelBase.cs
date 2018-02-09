using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Runtime.CompilerServices;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;
using Prism.Mvvm;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    public abstract class ViewModelBase : BindableBase, IViewModel, IValidatable, IModifiable
    {
        #region Constructors and Destructors

        /// <summary>
        /// Create a new instance of <see cref="ViewModelBase"/>.
        /// </summary>
        public ViewModelBase()
        {
            InitialPropertyValues = new Dictionary<string, object>();
            ValidationErrors = new Dictionary<string, IEnumerable<ValidationResult>>();
        }

        #endregion Constructors and Destructors

        #region Events

        /// <summary>
        /// Fire when errors changed.
        /// </summary>
        public event EventHandler<DataErrorsChangedEventArgs> ErrorsChanged;

        #endregion Events

        #region Members

        /// <summary>
        /// The initiale properties.
        /// </summary>
        protected virtual IDictionary<string, object> InitialPropertyValues { get; private set; }

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

        protected IDictionary<string, IEnumerable<ValidationResult>> ValidationErrors { get; private set; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Resets the modified member.
        /// </summary>
        public virtual void ResetModified()
        {
            var oldModified = IsModified;

            InitialPropertyValues.Clear();

            if (IsModified != oldModified)
            {
                RaisePropertyChanged(() => IsModified);
            }
        }

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
        /// Test if property is valid.
        /// </summary>
        /// <param name="propertyName">The property name.</param>
        /// <param name="value">The value.</param>
        /// <param name="raisePropertyErrorChanged"></param>
        /// <returns></returns>
        protected bool IsPropertyValid(string propertyName, object value, bool raisePropertyErrorChanged = true)
        {
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
        /// Raises this object's PropertyChanged event.
        /// </summary>
        protected void RaisePropertyChanged<T>(Expression<Func<T>> propertyExpression)
        {
            var propertyName = PropertySupport.ExtractPropertyName(propertyExpression);
            RaisePropertyChanged(propertyName);
        }

        /// <summary>
        /// Gets default name for OnChanged method for properties.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetOnPropertyChangedMethodName(string propertyName)
        {
            return "On" + propertyName + "Changed";
        }

        /// <summary>
        /// Gets default name for OnChanging method for properties.
        /// </summary>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        public static string GetOnPropertyChangingMethodName(string propertyName)
        {
            return "On" + propertyName + "Changing";
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected override bool SetProperty<T>(ref T storage, T value, [CallerMemberName] string propertyName = null)
        {
            return SetProperty(ref storage, value, null, propertyName);
        }

        /// <summary>
        /// Checks if a property already matches a desired value. Sets the property and
        /// notifies listeners only when necessary.
        /// </summary>
        /// <typeparam name="T">Type of the property.</typeparam>
        /// <param name="storage">Reference to a property with both getter and setter.</param>
        /// <param name="value">Desired value for the property.</param>
        /// <param name="propertyName">Name of the property used to notify listeners. This
        /// value is optional and can be provided automatically when invoked from compilers that
        /// support CallerMemberName.</param>
        /// <param name="onChanged">Action that is called after the property value has been changed.</param>
        /// <returns>True if the value was changed, false if the existing value matched the
        /// desired value.</returns>
        protected override bool SetProperty<T>(ref T storage, T value, Action onChanged, [CallerMemberName] string propertyName = null)
        {
            if (storage is IEntityViewModel)
            {
                if (ReferenceEquals(storage, value)) return false;
            }
            else if (Equals(storage, value)) return false;

            object initalValue;
            var oldModified = IsModified;

            // Has the property already changed from initial value ?
            if (propertyName != null && InitialPropertyValues.TryGetValue(propertyName, out initalValue))
            { // Yes.
                // Do we revert to the initial value ?
                if (Equals(value, initalValue))
                {
                    // Yes, so the remove it.
                    InitialPropertyValues.Remove(propertyName);
                }
            }
            else
            {
                // No, so we save the initial value.
                if (propertyName != null) InitialPropertyValues.Add(propertyName, storage);
            }

            // We check if the property is valid, which notifies if the property is in error.
            IsPropertyValid(propertyName, value);

            // Executes On<Property>Changing(T oldvalue, T newValue) method if exists
            var cancel = false;
            if (propertyName != null)
            {
                var type = GetType();
                var onChangedMethod = type
                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == GetOnPropertyChangingMethodName(propertyName) && x.GetParameters().Length == 1);

                if (onChangedMethod != null)
                {
                    var propertyChangingEventArgs = new PropertyChangingEventArgs<T>(storage, value);
                    var parameters = new object[1];
                    parameters[0] = propertyChangingEventArgs;

                    onChangedMethod.Invoke(this, parameters);
                    cancel = propertyChangingEventArgs.Cancel;
                }
            }

            if (!cancel)
                storage = value;

            // Executes On<Property>Changed() method if exists
            if (propertyName != null)
            {
                var type = GetType();
                var onChangedMethod = type
                    .GetMethods(BindingFlags.NonPublic | BindingFlags.Instance).FirstOrDefault(x => x.Name == GetOnPropertyChangedMethodName(propertyName) && x.GetParameters().Length == 0);

                if (onChangedMethod != null) onChangedMethod.Invoke(this, null);
            }

            // Action OnChanged
            if (onChanged != null) onChanged.Invoke();

            // Raise PropertyChanged Event
            RaisePropertyChanged(propertyName);

            // Raise PropertyChanged Event for IsModified Property
            if (IsModified != oldModified)
            {
                RaisePropertyChanged(() => IsModified);
            }

            return true;
        }

        /// <summary>
        /// Notify when error occured.
        /// </summary>
        /// <param name="propertyName">The property Name.</param>
        protected virtual void NotifyPropertyErrorChanged(string propertyName)
        {
            if (ErrorsChanged != null) ErrorsChanged(this, new DataErrorsChangedEventArgs(propertyName));
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

                if (collection == null) continue;

                if (collection.OfType<IValidatable>().Count(validatable => !validatable.IsValid(raisePropertyErrorChanged)) > 0)
                {
                    isValid = false;
                }
            }

            return isValid;
        }

        #endregion Methods
    }
}