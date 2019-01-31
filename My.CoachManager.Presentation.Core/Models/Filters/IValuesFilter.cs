using System.Collections;

namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public interface IValuesFilter : IFilter
    {
        /// <summary>
        /// Gets the available values used for filtering.
        /// </summary>
        /// <value>The values.</value>
        IEnumerable Values { get; }
    }
}