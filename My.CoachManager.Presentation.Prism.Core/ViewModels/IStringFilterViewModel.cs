namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    /// <summary>
    /// Provides members and properties to manage a filter.
    /// </summary>
    public interface IStringFilterViewModel : IFilterViewModel
    {
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        string Value { get; set; }
    }
}