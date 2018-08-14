using System;

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
            if (target == null)
            {
                return false;
            }

            var propertyInfo = target.GetType().GetProperty(PropertyName);
            if (propertyInfo == null) return false;
            var toCompare = propertyInfo.GetValue(target, null);

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