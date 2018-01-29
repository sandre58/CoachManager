﻿using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TAllowedValues"></typeparam>
    public class SelectedValueFilter<TValue, TAllowedValues> : SelectableFilter<TAllowedValues>, IValueFilter<TValue>
    {
        #region Fields

        /// <summary>
        /// instance of the value used in the comparison
        /// </summary>
        private TValue _value;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public SelectedValueFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedValueFilter(PropertyInfo propertyInfo, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyInfo, allowedValues)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SelectedValueFilter()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        public TValue Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        #endregion Members

        #region Methods

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

            var result = (value == null && Value == null) || (Value != null && Value.Equals(value));

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

        #endregion Methods

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Value", Value);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SelectedValueFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Value = (TValue)info.GetValue("Value", typeof(TValue));
        }

        #endregion ISerializable Implementation
    }
}