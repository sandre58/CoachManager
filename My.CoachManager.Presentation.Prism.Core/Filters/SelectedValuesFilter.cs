using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="TAllowedValues"></typeparam>
    public class SelectedValuesFilter<TAllowedValues> : SelectableFilter<TAllowedValues>, IValuesFilter
    {
        #region Fields

        /// <summary>
        /// instance of the value used in the comparison
        /// </summary>
        private IEnumerable _values;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValuesFilter{T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public SelectedValuesFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValuesFilter{T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedValuesFilter(PropertyInfo propertyInfo, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyInfo, allowedValues)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SelectedValuesFilter()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        public IEnumerable Values
        {
            get { return _values; }
            set { SetProperty(ref _values, value); }
        }

        #endregion Members

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        public override bool IsMatch(object target)
        {
            if (target == null)
            {
                return false;
            }
            var value = PropertyInfo.GetValue(target, null);

            var result = Values != null && Values.Cast<object>()
                             .Any(x => (x != null && x.Equals(value)) || (x == null && value == null));

            switch (Operator)
            {
                case BinaryOperator.Is:
                    return result;

                case BinaryOperator.IsNot:
                    return !result;

                default:
                    throw new NotImplementedException();
            }
        }

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Values", Values);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SelectedValuesFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Values = (IEnumerable)info.GetValue("Values", typeof(IEnumerable));
        }

        #endregion ISerializable Implementation
    }
}