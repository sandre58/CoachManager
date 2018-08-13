using System;
using My.CoachManager.CrossCutting.Core.Enums;

namespace My.CoachManager.CrossCutting.Core.Helpers
{
    public static class ComparableHelper
    {
        /// <summary>
        /// Compares two objects.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool Compare(IComparable x, IComparable y, ComparableOperator sign)
        {
            if (x == null || y == null)
            {
                return false;
            }

            var compare = x.CompareTo(y);

            switch (sign)
            {
                case ComparableOperator.EqualsTo:
                    return compare == 0;

                case ComparableOperator.NotEqualsTo:
                    return compare != 0;

                case ComparableOperator.LessThan:
                    return compare < 0;

                case ComparableOperator.GreaterThan:
                    return compare > 0;

                case ComparableOperator.LessEqualThan:
                    return compare <= 0;

                case ComparableOperator.GreaterEqualThan:
                    return compare >= 0;

                default:
                    throw new NotImplementedException();
            }
        }

        /// <summary>
        /// Compares three object.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="from"></param>
        /// <param name="to"></param>
        /// <param name="sign"></param>
        /// <returns></returns>
        public static bool Compare(IComparable x, IComparable from, IComparable to, ComplexComparableOperator sign)
        {
            if (x == null || from == null || to == null)
            {
                return false;
            }

            var compareFrom = x.CompareTo(from);
            var compareTo = x.CompareTo(to);

            bool result = (compareFrom >= 0 && compareTo <= 0);

            switch (sign)
            {
                case ComplexComparableOperator.IsBetween:
                    return result;

                case ComplexComparableOperator.IsNotBetween:
                    return !result;

                case ComplexComparableOperator.EqualsTo:
                    return compareFrom == 0;

                case ComplexComparableOperator.NotEqualsTo:
                    return compareFrom != 0;

                case ComplexComparableOperator.LessThan:
                    return compareTo < 0;

                case ComplexComparableOperator.GreaterThan:
                    return compareFrom > 0;

                case ComplexComparableOperator.LessEqualThan:
                    return compareTo <= 0;

                case ComplexComparableOperator.GreaterEqualThan:
                    return compareFrom >= 0;

                default:
                    throw new NotImplementedException();
            }
        }
    }
}