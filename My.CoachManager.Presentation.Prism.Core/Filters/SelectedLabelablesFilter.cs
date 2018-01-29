using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class SelectedLabelablesFilter : SelectedValuesFilter<ILabelableViewModel>
    {
        public SelectedLabelablesFilter(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedLabelablesFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedLabelablesFilter(PropertyInfo propertyInfo, IEnumerable<ILabelableViewModel> allowedValues)
            : base(propertyInfo, allowedValues)
        {
        }

        protected SelectedLabelablesFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SelectedLabelablesFilter()
        {
        }
    }
}