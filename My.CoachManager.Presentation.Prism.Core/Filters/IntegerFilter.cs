using System;
using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class IntegerFilter : ComparableFilter<int>
    {
        public IntegerFilter(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public IntegerFilter(PropertyInfo propertyInfo, ComparableOperator comparaison, int from, int to) : base(propertyInfo, comparaison, from, to)
        {
        }
    }
}