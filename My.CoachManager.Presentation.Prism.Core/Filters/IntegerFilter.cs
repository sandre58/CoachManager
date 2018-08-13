using System.Reflection;
using System.Runtime.Serialization;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class IntegerFilter : ComparableFilter<int>
    {
        public IntegerFilter(string propertyName) : base(propertyName)
        {
        }

        public IntegerFilter(string propertyName, ComplexComparableOperator comparaison, int from, int to) : base(
            propertyName, comparaison, from, to)
        {
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected IntegerFilter()
        {
        }
    }
}