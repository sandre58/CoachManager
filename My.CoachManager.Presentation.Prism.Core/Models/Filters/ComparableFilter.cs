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
                throw new ArgumentNullException(nameof(to));
            }
            if (from == null)
            {
                throw new ArgumentNullException(nameof(@from));
            }

            if (to != null && to.CompareTo(from) == -1)
            {
                throw new ArgumentException("Invalid input. <from> should be less or equal than <to>");
            }
            To = to;
            From = from;
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
        public ComplexComparableOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>From.</value>
        public T From { get; set; }

        /// <summary>
        /// Gets or sets he maximum value.
        /// </summary>
        /// <value>To.</value>
        public T To { get; set; }

        /// <summary>
        /// Gets or sets the minimum.
        /// </summary>
        /// <value>From.</value>
        public T Minimum { get; set; }

        /// <summary>
        /// Gets or sets he maximum.
        /// </summary>
        /// <value>To.</value>
        public T Maximum { get; set; }

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

            var compareTo = toComparable.CompareTo(To);
            var compareFrom = toComparable.CompareTo(From);

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
            if (!(obj is ComparableFilter<T> o) || GetType() != obj.GetType())
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

        #region PropertyChanged

        protected void OnToChanged()
        {
            if (To != null && To.CompareTo(From) < 0)
            {
                From = To;
            }
        }

        protected void OnFromChanged()
        {
            if (From != null && From.CompareTo(To) > 0)
            {
                To = From;
            }
        }

        #endregion

        #endregion Methods
    }
}