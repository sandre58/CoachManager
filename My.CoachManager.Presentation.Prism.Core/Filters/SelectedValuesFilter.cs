using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Security.Permissions;
using My.CoachManager.CrossCutting.Core.Enums;

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
        /// <param name="propertyName">The property info.</param>
        public SelectedValuesFilter(string propertyName)
            : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValuesFilter{T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedValuesFilter(string propertyName, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyName, allowedValues)
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
        /// <param name="toCompare">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsMatchProperty(object toCompare)
        {
            var result = Values != null && Values.Cast<object>()
                             .Any(x => (x != null && x.Equals(toCompare)) || (x == null && toCompare == null));

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
            return Values == null || !Values.Cast<object>().Any();
        }

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override void Reset()
        {
            Values = default(IEnumerable);
        }

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);

            if (Values != null)
            {
                info.AddValue("TypeValues", Values.Cast<object>().ToList().GetType());
                info.AddValue("Values", Values.Cast<object>().ToList());
            }
            else
            {
                info.AddValue("TypeValues", null);
            }
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SelectedValuesFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            var type = (Type)info.GetValue("TypeValues", typeof(Type));

            if (type != null)
            {
                Values = (IEnumerable)info.GetValue("Values", type);
            }
        }

        #endregion ISerializable Implementation
    }
}