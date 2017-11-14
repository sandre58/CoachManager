using System;

namespace My.CoachManager.CrossCutting.Core.Helpers
{
    /// <summary>
    /// Helper permettant de gérer les chaînes.
    /// </summary>
    public static class ConvertHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Convert an object in generic type.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="o"></param>
        /// <returns></returns>
        public static T ChangeType<T>(object o)
        {
            var conversionType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            return (T)Convert.ChangeType(o, conversionType);
        }

        #endregion Public Methods and Operators
    }
}