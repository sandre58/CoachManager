using System;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the contract for a range filter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IComparableFilter<T> : IFilter where T : IComparable
    {
        /// <summary>
        /// Gets or sets the minimum value.
        /// </summary>
        /// <value>From.</value>
        T From
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets he maximum value.
        /// </summary>
        /// <value>To.</value>
        T To
        {
            get;
            set;
        }
    }
}