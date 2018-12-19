using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <inheritdoc cref="ModelBase" /> 
    /// <summary>
    /// Base class for a filter
    /// </summary>
    public abstract class Filter : ModelBase, IFilter
    {
        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Prism.Core.Models.Filters.Filter" /> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        protected Filter(string propertyName) : this()
        {
            PropertyName = propertyName ?? throw new ArgumentNullException(nameof(propertyName));
        }

        protected Filter()
        {
        }

        #endregion Constructors

        #region Members

        /// <inheritdoc />
        /// <summary>
        /// Gets the property info whose property name is filtered
        /// </summary>
        /// <value>The property info.</value>
        public string PropertyName { get; set; }

        #endregion Members

        #region Methods

        /// <inheritdoc />
        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        public virtual bool IsMatch(object target)
        {
            return IsMatchInternal(target, PropertyName.Split('.'));
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="propertyNames"></param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        private bool IsMatchInternal(object target, IList<string> propertyNames)
        {
            if (target == null)
            {
                return false;
            }
            
            var toCompare = target;
            var newPropertyNames = propertyNames.ToList();

            foreach (var propertyName in propertyNames)
            {
                var propertyInfo = toCompare.GetType().GetProperty(propertyName);
                if (propertyInfo == null) return false;
                toCompare = propertyInfo.GetValue(toCompare, null);

                newPropertyNames.Remove(propertyName);

                if (newPropertyNames.Count > 0 && toCompare is IList toCompareEnumerableRecursive)
                {
                    return toCompareEnumerableRecursive.Cast<object>().Any(x => IsMatchInternal(x, newPropertyNames));
                }
            }

            if (toCompare is IList toCompareEnumerable)
            {
                return toCompareEnumerable.Cast<object>().Any(IsMatchProperty);
            }

            return IsMatchProperty(toCompare);
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="toCompare">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool IsMatchProperty(object toCompare);

        /// <inheritdoc />
        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public abstract bool IsEmpty();

        /// <inheritdoc />
        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public abstract void Reset();

        public override bool Equals(object obj)
        {
            if (!(obj is Filter o) || GetType() != obj.GetType())
            {
                return false;
            }
            return PropertyName == o.PropertyName;
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