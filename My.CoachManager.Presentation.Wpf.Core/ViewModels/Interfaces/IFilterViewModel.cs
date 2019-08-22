using System.ComponentModel;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Presentation.Core.Models.Filters;

namespace My.CoachManager.Presentation.Wpf.Core.ViewModels.Interfaces
{
    /// <summary>
    /// Provides members and properties to manage a filter.
    /// </summary>
    public interface IFilterViewModel : INotifyPropertyChanged
    {
        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        LogicalOperator Operator { get; set; }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        IFilter Filter { get; }

        /// <summary>
        /// Gets or sets a value indicates if filter is enabled.
        /// </summary>
        bool IsEnabled { get; set; }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        string Title { get; set; }
    }
}