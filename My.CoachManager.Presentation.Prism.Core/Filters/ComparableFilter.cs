using System;
using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the range filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class ComparableFilter<T> : Filter, IComparableFilter<T> where T : IComparable
    {
        #region Fields

        private T _from;

        private T _to;

        private ComparableOperator _operator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparableFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public ComparableFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparableFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="comparableOperator"></param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public ComparableFilter(PropertyInfo propertyInfo, ComparableOperator comparableOperator, T from, T to)
            : this(propertyInfo)
        {
            if (to == null)
            {
                throw new ArgumentNullException("to");
            }
            if (from == null)
            {
                throw new ArgumentNullException("from");
            }

            if (to != null && to.CompareTo(from) == -1)
            {
                throw new ArgumentException("Invalid input. <from> should be less or equal than <to>");
            }
            _to = to;
            _from = from;
            Operator = comparableOperator;
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public ComparableOperator Operator
        {
            get { return _operator; }
            set { SetProperty(ref _operator, value); }
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
                    if (_from != null && _from.CompareTo(_to) > 0)
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
                    if (_to != null && _to.CompareTo(_from) < 0)
                    {
                        From = _to;
                    }
                });
            }
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

            var toCompare = PropertyInfo.GetValue(target, null) as IComparable;

            if (toCompare == null)
            {
                return false;
            }

            var compareTo = toCompare.CompareTo(_to);
            var compareFrom = toCompare.CompareTo(_from);

            bool result = (compareFrom >= 0 && compareTo <= 0);

            switch (Operator)
            {
                case ComparableOperator.IsBetween:
                    return result;

                case ComparableOperator.IsNotBetween:
                    return !result;

                case ComparableOperator.EqualsTo:
                    return compareFrom == 0;

                case ComparableOperator.NotEqualsTo:
                    return compareFrom != 0;

                case ComparableOperator.LessThan:
                    return compareTo < 0;

                case ComparableOperator.GreaterThan:
                    return compareFrom > 0;

                case ComparableOperator.LessEqualThan:
                    return compareTo <= 0;

                case ComparableOperator.GreaterEqualThan:
                    return compareFrom >= 0;

                default:
                    throw new NotImplementedException();
            }
        }
    }

    #endregion Methods
}