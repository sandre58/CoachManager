using System;
using System.Globalization;

namespace My.CoachManager.CrossCutting.Core.Helpers
{
    /// <summary>
    /// Helper permettant de gérer les chaînes.
    /// </summary>
    public static class StringHelper
    {
        #region Public Methods and Operators

        /// <summary>
        /// Appelle <see cref="string.Format(IFormatProvider,string,object[])"/> avec <see cref="CultureInfo.InvariantCulture"/> en tant que fournisseur de format.
        /// </summary>
        /// <param name="format">La chaîne de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <param name="args">Les arguments de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <returns>Une chaîne formattée.</returns>
        public static string InvariantFormat(string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Appelle <see cref="string.Format(IFormatProvider,string,object[])"/> avec <see cref="CultureInfo.CurrentCulture"/> en tant que fournisseur de format.
        /// </summary>
        /// <param name="format">La chaîne de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <param name="args">Les arguments de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <returns>Une chaîne formattée.</returns>
        public static string UiFormat(string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentUICulture, format, args);
        }

        #endregion Public Methods and Operators
    }
}