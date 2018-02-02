using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using My.CoachManager.Presentation.Prism.Core.Filters;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels.Screens
{
    /// <summary>
    /// Provides members and properties to manage a filter.
    /// </summary>
    [Serializable]
    public class FilterViewModel : ViewModelBase, IFilterViewModel, IWorkspaceViewModel, ISerializable
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
        public FilterViewModel(IFilter filter, string title, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            Operator = logicalOperator;
            Filter = filter;

            IsEnabled = true;
            Title = title;
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected FilterViewModel()
        {
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
                SetProperty(ref _filter, value, () =>
                {
                    if (_filter != null)
                    {
                        _filter.PropertyChanged += (sender, args) => OnPropertyChanged(args);
                    }
                });
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

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Operator", Operator);
            info.AddValue("Filter", Filter);
            info.AddValue("IsEnabled", IsEnabled);
            info.AddValue("Title", Title);
            info.AddValue("Type", Filter.GetType());
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected FilterViewModel(SerializationInfo info, StreamingContext context)
        {
            var type = (Type)info.GetValue("Type", typeof(Type));
            Operator = (LogicalOperator)info.GetValue("Operator", typeof(LogicalOperator));
            Filter = (IFilter)info.GetValue("Filter", type);
            Title = info.GetString("Title");
            IsEnabled = info.GetBoolean("IsEnabled");
        }

        #endregion ISerializable Implementation

        #region Equals

        public override bool Equals(object obj)
        {
            var o = obj as FilterViewModel;

            if (Filter == null || o == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Filter.Equals(o.Filter);
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        #endregion Equals
    }
}