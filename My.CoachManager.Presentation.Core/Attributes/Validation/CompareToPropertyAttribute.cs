using System;
using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Core.Attributes.Validation
{
    [AttributeUsage(AttributeTargets.Property |
                    AttributeTargets.Field)]
    public class CompareToPropertyAttribute : ValidationAttribute
    {
        #region Members

        /// <summary>
        /// Gets property name.
        /// </summary>
        public string PropertyName
        {
            get;
        }

        /// <summary>
        /// Gets property name.
        /// </summary>
        public ComparableOperator Operator
        {
            get;
        }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ValidatePropertyAttribute"/>
        /// </summary>
        /// <param name="propertyName"></param>
        /// <param name="sign"></param>
        public CompareToPropertyAttribute(string propertyName, ComparableOperator sign)
        {
            PropertyName = propertyName;
            Operator = sign;
        }

        #endregion Constructors

        #region Methods

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var property = validationContext.ObjectType.GetProperty(PropertyName);

            if (property == null) return null;

            var otherValue = property.GetValue(validationContext.ObjectInstance);

            if (otherValue == null) return null;

            if (!(value as IComparable).Compare(otherValue as IComparable, Operator))
            {
                return new ValidationResult(FormatErrorMessage(ErrorMessage));
            }
            return null;
        }

        #endregion Methods
    }
}
