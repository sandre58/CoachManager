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
        private IFilter _filter;

        #endregion Fields

        #region Constructors

        /// <summary>
        /// Initialises a new instance of <see cref="FilterViewModel"/>.
        /// </summary>
        public FilterViewModel(string title, IFilter filter)
        {
            Title = title;
            Filter = filter;
            IsEnabled = true;
        }

        #endregion Constructors

        #region Members

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