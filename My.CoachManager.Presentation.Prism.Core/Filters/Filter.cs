using System;
using System.Reflection;
using System.Runtime.Serialization;
using System.Security.Permissions;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Base class for a filter
    /// </summary>
    [Serializable]
    public abstract class Filter : ViewModelBase, IFilter, ISerializable
    {
        #region Constructors

        /// <inheritdoc />
        /// <summary>
        /// Initializes a new instance of the <see cref="T:My.CoachManager.Presentation.Prism.Core.Filters.Filter" /> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        protected Filter(PropertyInfo propertyInfo) : this()
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException("propertyInfo");
            }
            PropertyInfo = propertyInfo;
        }

        protected Filter()
        {
            Id = Guid.NewGuid();
        }

        #endregion Constructors

        #region Members

        /// <summary>
        /// Gets the property info whose property name is filtered
        /// </summary>
        /// <value>The property info.</value>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        /// Gets or set uniq id.
        /// </summary>
        public Guid Id { get; }

        #endregion Members

        #region Methods

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        public abstract bool IsMatch(object target);

        public override bool Equals(object obj)
        {
            var o = obj as Filter;

            if (o == null || GetType() != obj.GetType())
            {
                return false;
            }
            return Id == o.Id;
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        #endregion Methods

        #region ISerializable Implementation

        /// <summary>
        /// Save data for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        [SecurityPermission(SecurityAction.Demand, SerializationFormatter = true)]
        public virtual void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("PropertyInfo", PropertyInfo);
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected Filter(SerializationInfo info, StreamingContext context)
        {
            PropertyInfo = (PropertyInfo)info.GetValue("PropertyInfo", typeof(PropertyInfo));
        }

        #endregion ISerializable Implementation
    }
}