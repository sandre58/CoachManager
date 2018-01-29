using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.Serialization;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class SelectedLabelableFilter : SelectedValueFilter<int, ILabelableViewModel>
    {
        public SelectedLabelableFilter(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedLabelableFilter"/> class.
        /// </summary>
        /// <param name="propertyInfo">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedLabelableFilter(PropertyInfo propertyInfo, IEnumerable<ILabelableViewModel> allowedValues)
            : base(propertyInfo, allowedValues)
        {
        }

        protected SelectedLabelableFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SelectedLabelableFilter()
        {
        }
    }
}