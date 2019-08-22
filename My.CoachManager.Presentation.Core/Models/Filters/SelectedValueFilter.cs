using System;
using System.Collections.Generic;

using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    /// <typeparam name="TAllowedValues"></typeparam>
    public class SelectedValueFilter<TValue, TAllowedValues> : SelectableFilter<TAllowedValues>, IValueFilter<TValue>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="isFixed"></param>
        public SelectedValueFilter(string propertyName, bool isFixed = false)
            : base(propertyName,null,isFixed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        /// <param name="isFixed"></param>
        public SelectedValueFilter(string propertyName, IEnumerable<TAllowedValues> allowedValues, bool isFixed = false)
            : base(propertyName, allowedValues,isFixed)
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
        public TValue Value { get; set; }

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
