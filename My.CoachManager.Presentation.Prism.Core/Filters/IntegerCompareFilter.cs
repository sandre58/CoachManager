using System;
using System.Reflection;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class IntegerCompareFilter : CompareFilter<int>
    {
        public IntegerCompareFilter(PropertyInfo propertyInfo) : base(propertyInfo)
        {
        }

        public IntegerCompareFilter(PropertyInfo propertyInfo, ComparaisonType comparaison, int value) : base(propertyInfo, comparaison, value)
        {
        }
    }
}