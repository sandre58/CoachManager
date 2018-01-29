using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="TAllowedValues"></typeparam>
    public abstract class SelectableFilter<TAllowedValues> : Filter
    {
        #region Fields

        private IEnumerable<TAllowedValues> _allowedValues;

        private BinaryOperator _operator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public SelectableFilter(PropertyInfo propertyInfo)
            : this(propertyInfo, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectableFilter(PropertyInfo propertyInfo, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyInfo)
        {
            Operator = BinaryOperator.Is;
            AllowedValues = allowedValues;
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SelectableFilter()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public BinaryOperator Operator
        {
            get { return _operator; }
            set { SetProperty(ref _operator, value); }
        }

        /// <summary>
        /// Gets the values to check for equality
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<TAllowedValues> AllowedValues
        {
            get { return _allowedValues; }
            set { SetProperty(ref _allowedValues, value); }
        }

        #endregion Members

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Operator", Operator);
            info.AddValue("AllowedValues", AllowedValues);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SelectableFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Operator = (BinaryOperator)info.GetValue("Operator", typeof(BinaryOperator));
            AllowedValues = (IEnumerable<TAllowedValues>)info.GetValue("AllowedValues", typeof(IEnumerable<TAllowedValues>));
        }

        #endregion ISerializable Implementation
    }
}