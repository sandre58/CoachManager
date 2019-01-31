using System.Collections.Generic;
using My.CoachManager.Presentation.Core.Models.Filters;

namespace My.CoachManager.Presentation.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class SquadsFilter : SelectedValuesFilter<SquadModel>
    {

        #region Constructors

        public SquadsFilter(string propertyName) : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedValuesFilter{T}"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        public SquadsFilter(string propertyName, IEnumerable<SquadModel> allowedValues)
            : base(propertyName, allowedValues)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected SquadsFilter()
        {
        }

        #endregion

    }
}