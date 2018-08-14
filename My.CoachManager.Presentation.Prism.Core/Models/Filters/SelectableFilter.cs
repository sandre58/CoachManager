using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Security.Permissions;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    /// <typeparam name="TAllowedValues"></typeparam>
    public abstract class SelectableFilter<TAllowedValues> : Filter, ISerializable
    {
        #region Fields

        private IEnumerable<TAllowedValues> _allowedValues;

        private BinaryOperator _operator;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        public SelectableFilter(string propertyName)
            : this(propertyName, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValueFilter{T,T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectableFilter(string propertyName, IEnumerable<TAllowedValues> allowedValues)
            : base(propertyName)
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

        public override bool Equals(object obj)
        {
            var o = obj as SelectableFilter<TAllowedValues>;

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

        #endregion Members

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Operator", Operator);
            info.AddValue("PropertyName", PropertyName);
            info.AddValue("Type", AllowedValues.GetType());
            info.AddValue("AllowedValues", AllowedValues);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SelectableFilter(SerializationInfo info, StreamingContext context)
        {
            var type = (Type)info.GetValue("Type", typeof(Type));
            Operator = (BinaryOperator)info.GetValue("Operator", typeof(BinaryOperator));
            AllowedValues = (IEnumerable<TAllowedValues>)info.GetValue("AllowedValues", type);
            PropertyName = info.GetString("PropertyName");
        }

        #endregion ISerializable Implementation
    }
}