using System;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
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

        private T _minimum;

        private T _maximum;

        private ComplexComparableOperator _operator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparableFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        public ComparableFilter(string propertyName)
            : base(propertyName)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ComparableFilter&lt;T&gt;"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="comparableOperator"></param>
        /// <param name="from">From.</param>
        /// <param name="to">To.</param>
        public ComparableFilter(string propertyName, ComplexComparableOperator comparableOperator, T from, T to)
            : this(propertyName)
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

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected ComparableFilter()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public ComplexComparableOperator Operator
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

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>From.</value>
        public T Minimum
        {
            get
            {
                return _minimum;
            }
            set
            {
                SetProperty(ref _minimum, value);
            }
        }

        /// <summary>
        /// Gets or sets he maximum.
        /// </summary>
        /// <value>To.</value>
        public T Maximum
        {
            get
            {
                return _maximum;
            }
            set
            {
                SetProperty(ref _maximum, value);
            }
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
            if (!(toCompare is IComparable toComparable))
            {
                return false;
            }

            var compareTo = toComparable.CompareTo(_to);
            var compareFrom = toComparable.CompareTo(_from);

            bool result = (compareFrom >= 0 && compareTo <= 0);

            switch (Operator)
            {
                case ComplexComparableOperator.IsBetween:
                    return result;

                case ComplexComparableOperator.IsNotBetween:
                    return !result;

                case ComplexComparableOperator.EqualsTo:
                    return compareFrom == 0;

                case ComplexComparableOperator.NotEqualsTo:
                    return compareFrom != 0;

                case ComplexComparableOperator.LessThan:
                    return compareTo < 0;

                case ComplexComparableOperator.GreaterThan:
                    return compareFrom > 0;

                case ComplexComparableOperator.LessEqualThan:
                    return compareTo <= 0;

                case ComplexComparableOperator.GreaterEqualThan:
                    return compareFrom >= 0;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override bool IsEmpty()
        {
            return (From == null || From.Equals(default(T))) && (To == null || To.Equals(default(T)));
        }

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override void Reset()
        {
            From = default(T);
            To = default(T);
        }

        public override bool Equals(object obj)
        {
            var o = obj as ComparableFilter<T>;

            if (o == null || GetType() != obj.GetType())
            {
                return false;
            }

            return base.Equals(obj) && Operator == o.Operator;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        #endregion Methods
    }
}