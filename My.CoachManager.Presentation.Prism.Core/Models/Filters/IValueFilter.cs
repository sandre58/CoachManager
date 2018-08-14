namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValueFilter<T> : IFilter
    {
        /// <summary>
        /// Gets the available value used for filtering.
        /// </summary>
        /// <value>The values.</value>
        T Value { get; set; }
    }
}