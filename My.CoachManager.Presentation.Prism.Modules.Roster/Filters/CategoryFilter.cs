using System.Reflection;
using My.CoachManager.Presentation.Prism.Core.Filters;
using My.CoachManager.Presentation.Prism.ViewModels;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Filters
{
    /// <summary>
    /// Defines a string filter
    /// </summary>
    public class CategoryFilter : EqualMultiValuesFilter<CategoryViewModel>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StringFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        public CategoryFilter(PropertyInfo propertyInfo)
            : base(propertyInfo)
        {
        }
    }
}