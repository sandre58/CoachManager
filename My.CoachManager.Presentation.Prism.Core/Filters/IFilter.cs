using System;
using System.ComponentModel;
using System.Reflection;
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
        PropertyInfo PropertyInfo
        {
            get;
        }

        /// <summary>
        /// Determines whether the specified target is matching the criteria
        /// </summary>
        /// <param name="target">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is matching the criteria; otherwise, <c>false</c>.
        /// </returns>
        bool IsMatch(object target);
    }

    /// <summary>
    /// Generics
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IFilter<T> : IFilter
        where T : IComparable
    { }
}