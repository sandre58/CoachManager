using System;
using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class CompareFilter<T> : Filter, IValueFilter<T> where T : IComparable
    {
        /// <summary>
        /// instance of the value used in the comparison
        /// </summary>
        private T _value;

        private ComparaisonType _comparaison;

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public CompareFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="CompareFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="comparaison"></param>
        /// <param name="value">The compare to.</param>
        public CompareFilter(PropertyInfo propertyInfo, ComparaisonType comparaison, T value)
            : this(propertyInfo)
        {
            if (value == null)
            {
                throw new ArgumentNullException($"compareTo");
            }

            _comparaison = comparaison;
            _value = value;
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
            T value = (T)PropertyInfo.GetValue(target, null);

            var compare = value.CompareTo(_value);

            switch (_comparaison)
            {
                case ComparaisonType.NotEqualsTo:
                    return compare != 0;

                case ComparaisonType.LessThan:
                    return compare < 0;

                case ComparaisonType.GreaterThan:
                    return compare > 0;

                case ComparaisonType.LessEqualThan:
                    return compare <= 0;

                case ComparaisonType.GreaterEqualThan:
                    return compare >= 0;

                default:
                    return compare == 0;
            }
        }

        /// <summary>
        /// Gets or sets the value used in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        public T Value
        {
            get { return _value; }
            set { SetProperty(ref _value, value); }
        }

        /// <summary>
        /// Gets or sets the type in the comparison.
        /// </summary>
        /// <value>The compare to.</value>
        public ComparaisonType Comparaison
        {
            get { return _comparaison; }
            set { SetProperty(ref _comparaison, value); }
        }
    }
}