using System;
using System.Collections.Generic;
using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="TAllowedValues"></typeparam>
    public abstract class SelectableFilter<TAllowedValues> : Filter
    {
        #region Fields

        private IEnumerable<TAllowedValues> _allowedValues;

        private BinaryOperator _operator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public SelectableFilter(PropertyInfo propertyInfo)
            : this(propertyInfo, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectableFilter(PropertyInfo propertyInfo, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyInfo)
        {
            Operator = BinaryOperator.Is;
            AllowedValues = allowedValues;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public BinaryOperator Operator
        {
            get { return _operator; }
            set { SetProperty(ref _operator, value); }
        }

        /// <summary>
        /// Gets the values to check for equality
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<TAllowedValues> AllowedValues
        {
            get { return _allowedValues; }
            set { SetProperty(ref _allowedValues, value); }
        }

        #endregion Members
    }
}