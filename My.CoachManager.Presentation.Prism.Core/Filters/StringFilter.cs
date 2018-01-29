﻿using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines a string filter
    /// </summary>
    [Serializable]
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
        /// <param name="propertyInfo">The property info.</param>
        public StringFilter(PropertyInfo propertyInfo)
            : this(propertyInfo, StringOperator.Contains, false)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="filterMode">The filter mode.</param>
        /// <param name="caseSensitive"></param>
        public StringFilter(PropertyInfo propertyInfo, StringOperator filterMode, bool caseSensitive)
            : base(propertyInfo)
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
        /// <param name="target">The target.</param>
        /// <exception cref="NotImplementedException"></exception>
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
                if (Operator == StringOperator.Is)
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

            switch (Operator)
            {
                case StringOperator.Contains:
                    return toCompare.Contains(value);

                case StringOperator.StartsWith:
                    return toCompare.StartsWith(value);

                case StringOperator.EndsWith:
                    return toCompare.EndsWith(value);

                case StringOperator.Is:
                    return toCompare.Equals(value);

                case StringOperator.IsNot:
                    return !toCompare.Equals(value);

                default:
                    throw new NotImplementedException();
            }
        }

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
            info.AddValue("Operator", Operator);
            info.AddValue("Value", Value);
            info.AddValue("CaseSensitive", CaseSensitive);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected StringFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            Operator = (StringOperator)info.GetValue("Operator", typeof(StringOperator));
            Value = info.GetString("Value");
            CaseSensitive = info.GetBoolean("CaseSensitive");
        }

        #endregion ISerializable Implementation
    }
}