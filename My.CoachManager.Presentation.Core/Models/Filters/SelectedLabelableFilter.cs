﻿using System.Collections.Generic;

namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class SelectedLabelableFilter : SelectedValueFilter<int, IReferenceModel>
    {
        public SelectedLabelableFilter(string propertyName) : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedLabelableFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        /// <param name="isFixed"></param>
        public SelectedLabelableFilter(string propertyName, IEnumerable<IReferenceModel> allowedValues, bool isFixed = false)
            : base(propertyName, allowedValues,isFixed)
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
