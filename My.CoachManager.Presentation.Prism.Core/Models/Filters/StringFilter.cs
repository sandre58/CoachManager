using System;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <summary>
    /// Defines a string filter
    /// </summary>
    public class StringFilter : Filter, IValueFilter<string>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="caseSensitive"></param>
        public StringFilter(string propertyName, StringOperator filterMode = StringOperator.Contains, bool caseSensitive = false)
            : base(propertyName)
        {
            Operator = filterMode;
            CaseSensitive = caseSensitive;
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected StringFilter()
        {
        }

        #endregion Constructors

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public StringOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public string Value { get; set; }

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public bool CaseSensitive { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="toCompare">The target.</param>
        /// <exception cref="T:System.NotImplementedException"></exception>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsMatchProperty(object toCompare)
        {
            if (toCompare == null)
            {
                if (Operator == StringOperator.Is)
                {
                    return Value == null;
                }
                return false;
            }

            var toStringCompare = toCompare.ToString();

            if (Value == null)
            {
                return false;
            }

            var value = Value;
            if (!CaseSensitive)
            {
                value = Value.ToUpper();
                toStringCompare = toStringCompare.ToUpper();
            }

            switch (Operator)
            {
                case StringOperator.Contains:
                    return toStringCompare.Contains(value);

                case StringOperator.StartsWith:
                    return toStringCompare.StartsWith(value);

                case StringOperator.EndsWith:
                    return toStringCompare.EndsWith(value);

                case StringOperator.Is:
                    return toStringCompare.Equals(value);

                case StringOperator.IsNot:
                    return !toStringCompare.Equals(value);

                default:
                    throw new NotImplementedException();
            }
        }
        
        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override bool IsEmpty()
        {
            return string.IsNullOrEmpty(Value);
        }
        
        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override void Reset()
        {
            Value = string.Empty;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is StringFilter o) || GetType() != obj.GetType())
            {
                return false;
            }
            return base.Equals(obj) && Operator == o.Operator && CaseSensitive == o.CaseSensitive;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }
    }
}