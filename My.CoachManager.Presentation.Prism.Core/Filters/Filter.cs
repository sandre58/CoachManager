using System;
using System.Reflection;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Base class for a filter
    /// </summary>
    public abstract class Filter : ViewModelBase, IFilter
    {
        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Prism.Core.Filters.Filter" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        protected Filter(PropertyInfo propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            PropertyInfo = propertyInfo;
        }

        /// <summary>
        /// Gets the property info whose property name is filtered
        /// </summary>
        /// <value>The property info.</value>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMatch(object target);
    }
}