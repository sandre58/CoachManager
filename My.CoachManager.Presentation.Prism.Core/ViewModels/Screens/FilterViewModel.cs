using My.CoachManager.CrossCutting.Core.Extensions;
using My.CoachManager.Presentation.Prism.Core.Filters;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    /// <summary>
    /// Provides members and properties to manage a filter.
    /// </summary>
    public class FilterViewModel : ViewModelBase, IWorkspaceViewModel
    {
        #region Fields

        private string _title;
        private bool _isEnabled;
        private LogicalOperator _operator;
        private IFilter _filter;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises a new instance of <see cref="FilterViewModel"/>.
        /// </summary>
        public FilterViewModel(IFilter filter, string title = "", LogicalOperator logicalOperator = LogicalOperator.And)
        {
            Operator = logicalOperator;
            Filter = filter;
            Filter.PropertyChanged += (sender, args) => OnPropertyChanged(args);

            IsEnabled = true;

            if (string.IsNullOrEmpty(title))
            {
                if (filter != null)
                {
                    var name = filter.PropertyInfo.GetDisplayName();
                    Title = !string.IsNullOrEmpty(name) ? name : filter.PropertyInfo.Name;
                }
            }
            else
            {
                Title = title;
            }
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public LogicalOperator Operator
        {
            get { return _operator; }
            set { SetProperty(ref _operator, value); }
        }

        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public IFilter Filter
        {
            get { return _filter; }
            private set
            {
                SetProperty(ref _filter, value);
            }
        }

        /// <summary>
        /// Gets or sets a value indicates if filter is enabled.
        /// </summary>
        public bool IsEnabled
        {
            get { return _isEnabled; }
            set { SetProperty(ref _isEnabled, value); }
        }

        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        #endregion Members
    }
}