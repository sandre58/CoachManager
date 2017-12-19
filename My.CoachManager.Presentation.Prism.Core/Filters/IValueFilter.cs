using System;
using System.Collections.Generic;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueFilter<T> : IFilter<T> where T : IComparable
    {
        /// <summary>
        /// Gets the available value used for filtering.
        /// </summary>
        /// <value>The values.</value>
        T Value { get; set; }
    }
}