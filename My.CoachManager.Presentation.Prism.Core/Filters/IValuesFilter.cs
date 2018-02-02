using System.Collections;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Prism.Core.Filters
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