using System;

using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class DateFilter : ComparableFilter<DateTime>
    {
        public DateFilter(string propertyName, bool isFixed = false) : base(propertyName,isFixed)
        {
        }

        public DateFilter(string propertyName, ComplexComparableOperator comparaison, DateTime from, DateTime to, bool isFixed = false) : base(
            propertyName, comparaison, from, to,isFixed)
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
