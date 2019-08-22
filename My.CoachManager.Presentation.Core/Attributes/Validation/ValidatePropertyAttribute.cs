using System;

namespace My.CoachManager.Presentation.Core.Attributes.Validation
{
    /// <summary>
    /// Indicates that the specified property must be validate in same time this property.
    /// </summary>
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = true)]
    public class ValidatePropertyAttribute : Attribute
    {
        #region Members

        /// <summary>
        /// Gets property name.
        /// </summary>
        public string PropertyName
        {
            get;
        }

        #endregion Members

        #region Constructors

        /// <summary>
        /// Initialise a new instance of <see cref="ValidatePropertyAttribute"/>
        /// </summary>
        /// <param name="propertyName"></param>
        public ValidatePropertyAttribute(string propertyName)
        {
            PropertyName = propertyName;
        }

        #endregion Constructors
    }
}
