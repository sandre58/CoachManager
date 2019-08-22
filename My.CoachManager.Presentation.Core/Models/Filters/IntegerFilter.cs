using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class IntegerFilter : ComparableFilter<int>
    {
        public IntegerFilter(string propertyName, bool isFixed = false) : base(propertyName,isFixed)
        {
        }

        public IntegerFilter(string propertyName, ComplexComparableOperator comparaison, int from, int to, bool isFixed = false) : base(
            propertyName, comparaison, from, to,isFixed)
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
