using System;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Base class for a filter
    /// </summary>
    public abstract class Filter : ViewModelBase, IFilter
    {
        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Prism.Core.Filters.Filter" /> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        protected Filter(string propertyName) : this()
        {
            if (propertyName == null)
            {
                throw new ArgumentNullException($"propertyName");
            }
            PropertyName = propertyName;
        }

        protected Filter()
        {
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets the property info whose property name is filtered
        /// </summary>
        /// <value>The property info.</value>
        public string PropertyName { get; set; }

        #endregion Members

        #region Methods

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
            if (propertyInfo != null)
            {
                var toCompare = propertyInfo.GetValue(target, null);

                return IsMatchProperty(toCompare);
            }

            return false;
        }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="toCompare">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        protected abstract bool IsMatchProperty(object toCompare);

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public abstract bool IsEmpty();

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        public abstract void Reset();

        public override bool Equals(object obj)
        {
            var o = obj as Filter;

            if (o == null || GetType() != obj.GetType())
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