using System.Reflection;
using System.Runtime.Serialization;

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

        protected IntegerFilter(SerializationInfo info, StreamingContext context) : base(info, context)
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