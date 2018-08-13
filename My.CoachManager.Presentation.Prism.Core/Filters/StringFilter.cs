using System;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines a string filter
    /// </summary>
    public class StringFilter : Filter, IValueFilter<string>
    {
        #region Fields

        private bool _caseSensitive;

        private string _value;

        private StringOperator _operator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        public StringFilter(string propertyName)
            : this(propertyName, StringOperator.Contains, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="caseSensitive"></param>
        public StringFilter(string propertyName, StringOperator filterMode, bool caseSensitive)
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
        public StringOperator Operator
        {
            get { return _operator; }
            set { SetProperty(ref _operator, value); }
        }

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get
            {
                return _value;
            }
            set { SetProperty(ref _value, value); }
        }

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public bool CaseSensitive
        {
            get
            {
                return _caseSensitive;
            }
            set { SetProperty(ref _caseSensitive, value); }
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="toCompare">The target.</param>
        /// <exception cref="NotImplementedException"></exception>
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

            var value = _value;
            if (!_caseSensitive)
            {
                value = _value.ToUpper();
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
            var o = obj as StringFilter;

            if (o == null || GetType() != obj.GetType())
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