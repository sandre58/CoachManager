using System;
using System.Runtime.Serialization;
using System.Security.Permissions;
using My.CoachManager.Presentation.Prism.Core.Models;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Interfaces;

namespace My.CoachManager.Presentation.Prism.Core.ViewModels
{
    /// <summary>
    /// Provides members and properties to manage a filter.
    /// </summary>
    [Serializable]
    public class FilterViewModel : ModelBase, IFilterViewModel, ISerializable
    {

        #region Constructors

        /// <summary>
        /// Initialises a new instance of <see cref="FilterViewModel"/>.
        /// </summary>
        public FilterViewModel(IFilter filter, string title, LogicalOperator logicalOperator = LogicalOperator.And)
        {
            Operator = logicalOperator;
            Filter = filter;
            Filter.PropertyChanged += delegate
            {
                RaisePropertyChanged(() => Filter);
            };

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

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the operator.
        /// </summary>
        /// <value>The property info.</value>
        public LogicalOperator Operator { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        public IFilter Filter { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets a value indicates if filter is enabled.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <inheritdoc />
        /// <summary>
        /// Gets or sets the title.
        /// </summary>
        public string Title { get; set; }

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
            if (Filter == null || !(obj is FilterViewModel o) || GetType() != obj.GetType())
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