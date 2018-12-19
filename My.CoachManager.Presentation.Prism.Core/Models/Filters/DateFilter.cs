using System;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class DateFilter : ComparableFilter<DateTime>
    {
        public DateFilter(string propertyName) : base(propertyName)
        {
        }

        public DateFilter(string propertyName, ComplexComparableOperator comparaison, DateTime from, DateTime to) : base(
            propertyName, comparaison, from, to)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected DateFilter()
        {
        }
    }
}