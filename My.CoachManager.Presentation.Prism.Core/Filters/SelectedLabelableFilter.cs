﻿using System.Collections.Generic;
using My.CoachManager.Presentation.Prism.Core.ViewModels.Entities;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class SelectedLabelableFilter : SelectedValueFilter<int, ILabelableViewModel>
    {
        public SelectedLabelableFilter(string propertyName) : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="SelectedLabelableFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="allowedValues"></param>
        public SelectedLabelableFilter(string propertyName, IEnumerable<ILabelableViewModel> allowedValues)
            : base(propertyName, allowedValues)
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