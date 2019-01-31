namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    /// Defines a string filter
    /// </summary>
    public class BooleanFilter : Filter, IValueFilter<bool>
    {

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        public BooleanFilter(string propertyName)
            : base(propertyName)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected BooleanFilter()
        {
        }

        #endregion Constructors

        /// <summary>
        /// Gets or sets the value to look for.
        /// </summary>
        /// <value>The value.</value>
        public bool Value { get; set; }

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
            return (bool)toCompare == Value;
        }
        
        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override bool IsEmpty()
        {
            return Value == false;
        }
        
        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public override void Reset()
        {
            Value = false;
        }

        public override bool Equals(object obj)
        {
            if (!(obj is BooleanFilter) || GetType() != obj.GetType())
            {
                return false;
            }
            return base.Equals(obj);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }
    }
}