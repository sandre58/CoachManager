using System.Collections.Generic;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IMultiValueFilter<T> : IFilter
    {
        /// <summary>
        /// Gets the available values used for filtering.
        /// </summary>
        /// <value>The values.</value>
        IList<T> Values { get; }
    }
}