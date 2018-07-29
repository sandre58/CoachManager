using System;
using System.ComponentModel;
using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the range filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class RangeFilter<T> : Filter, IRangeFilter<T>
        where T : IComparable
    {
        /// <summary>
        /// Minimum value
        /// </summary>
        private T _from;

        /// <summary>
        /// Maximum value
        /// </summary>
        private T _to;

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public RangeFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="RangeFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public RangeFilter(PropertyInfo propertyInfo, T from, T to)
            : base(propertyInfo)
        {
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to.CompareTo(from) == -1)
            {
                throw new ArgumentException("Invalid input. <from> should be less or equal than <to>");
            }
            _to = to;
            _from = from;
        }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>From.</value>
        public T From
        {
            get
            {
                return _from;
            }
            set
            {
                SetProperty(ref _from, value, () =>
                {
                    if (_from.CompareTo(_to) < 0)
                    {
                        To = _from;
                    }
                });
            }
        }

        /// <summary>
        /// Gets or sets he maximum value.
        /// </summary>
        /// <value>To.</value>
        public T To
        {
            get
            {
                return _to;
            }
            set
            {
                SetProperty(ref _to, value, () =>
                {
                    if (_to.CompareTo(_from) < 0)
                    {
                        From = _to;
                    }
                });
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

            T value = (T)PropertyInfo.GetValue(target, null);

            bool result = (value.CompareTo(_from) >= 0 && value.CompareTo(_to) <= 0);
            return result;
        }
    }
}