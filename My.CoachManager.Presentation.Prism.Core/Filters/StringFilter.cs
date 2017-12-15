using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines a string filter
    /// </summary>
    public class StringFilter : Filter
    {
        /// <summary>
        /// The comparison mode
        /// </summary>
        private StringFilterMode _filterMode;

        /// <summary>
        /// Case sensitive.
        /// </summary>
        private bool _caseSensitive;

        /// <summary>
        /// value to search for
        /// </summary>
        private string _value;

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="caseSensitive"></param>
        public StringFilter(PropertyInfo propertyInfo, StringFilterMode filterMode, bool caseSensitive = false)
            : base(propertyInfo)
        {
            _filterMode = filterMode;
            _caseSensitive = caseSensitive;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="caseSensitive"></param>
        /// <param name="value">The value.</param>
        public StringFilter(PropertyInfo propertyInfo, StringFilterMode filterMode, bool caseSensitive, string value)
            : this(propertyInfo, filterMode, caseSensitive)
        {
            _value = value;
        }

        /// <summary>
        /// Gets or sets the mode.
        /// </summary>
        /// <value>The mode.</value>
        public StringFilterMode Mode
        {
            get
            {
                return _filterMode;
            }
            set
            {
                _filterMode = value;
                RaiseFilteringChanged();
            }
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
            set
            {
                if (_value != value)
                {
                    _value = value;
                    RaiseFilteringChanged();
                }
            }
        }

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
            string toCompare = (string)PropertyInfo.GetValue(target, null);

            if (toCompare == null)
            {
                if (_filterMode == StringFilterMode.Equals)
                {
                    return _value == null;
                }
                return false;
            }

            if (_value == null)
            {
                return false;
            }

            var value = _value;
            if (!_caseSensitive)
            {
                value = _value.ToUpper();
                toCompare = toCompare.ToUpper();
            }

            switch (_filterMode)
            {
                case StringFilterMode.Contains:
                    return toCompare.Contains(value);

                case StringFilterMode.StartsWith:
                    return toCompare.StartsWith(value);

                case StringFilterMode.EndsWith:
                    return toCompare.EndsWith(value);

                default:
                    return toCompare.Equals(value);
            }
        }
    }
}