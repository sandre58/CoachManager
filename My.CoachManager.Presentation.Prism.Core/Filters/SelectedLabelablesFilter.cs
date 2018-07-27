using System.Collections.Generic;
using System.Runtime.Serialization;
using My.CoachManager.Presentation.Prism.Core.Models;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class SelectedLabelablesFilter : SelectedValuesFilter<IReferenceModel>
    {
        public SelectedLabelablesFilter(string propertyName) : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedLabelablesFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedLabelablesFilter(string propertyName, IEnumerable<IReferenceModel> allowedValues)
            : base(propertyName, allowedValues)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SelectedLabelablesFilter()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected SelectedLabelablesFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }
    }
}