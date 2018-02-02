﻿using System.ComponentModel;
using My.CoachManager.Presentation.Prism.Core.ViewModels;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    /// Defines the contract for a filter used by the FilteredCollection
    /// </summary>
    public interface IFilter : IViewModel, INotifyPropertyChanged
    {
        /// <summary>
        /// Gets the property info.
        /// </summary>
        /// <value>The property info.</value>
        string PropertyName
        {
            get;
            set;
        }

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        bool IsEmpty();

        /// <summary>
        /// Gets if the filter is empty.
        /// </summary>
        void Reset();

        /// <summary>
        /// Determines whether the specified target is matching the criteria
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is matching the criteria; otherwise, <c>false</c>.
        /// </returns>
        bool IsMatch(object target);
    }
}