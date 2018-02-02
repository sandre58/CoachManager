﻿using System;
using System.Collections.Generic;

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
        /// <param name="propertyName">The property info.</param>
        public SelectedValueFilter(string propertyName)
            : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedValueFilter(string propertyName, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyName, allowedValues)
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
        /// <param name="toCompare">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsMatchProperty(object toCompare)
        {
            var result = (toCompare == null && Value == null) || (Value != null && Value.Equals(toCompare));

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

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override bool IsEmpty()
        {
            return Value == null || Value.Equals(default(TValue));
        }

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override void Reset()
        {
            Value = default(TValue);
        }

        #endregion Methods
    }
}