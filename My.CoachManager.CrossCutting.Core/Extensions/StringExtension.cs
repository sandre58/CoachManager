using System;
using System.Globalization;
using System.Linq;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class StringExtension
    {
        public static string FirstCharToUpper(this string input)
        {
            switch (input)
            {
                case null: throw new ArgumentNullException(nameof(input));
                case "": throw new ArgumentException($@"{nameof(input)} cannot be empty", nameof(input));
                default: return input.First().ToString().ToUpper() + input.Substring(1);
            }
        }

        /// <summary>
        /// Appelle <see cref="string.Format(IFormatProvider,string,object[])"/> avec <see cref="CultureInfo.InvariantCulture"/> en tant que fournisseur de format.
        /// </summary>
        /// <param name="format">La chaîne de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <param name="args">Les arguments de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <returns>Une chaîne formattée.</returns>
        public static string InvariantFormat(this string format, params object[] args)
        {
            return string.Format(CultureInfo.InvariantCulture, format, args);
        }

        /// <summary>
        /// Appelle <see cref="string.Format(IFormatProvider,string,object[])"/> avec <see cref="CultureInfo.CurrentCulture"/> en tant que fournisseur de format.
        /// </summary>
        /// <param name="format">La chaîne de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <param name="args">Les arguments de formattage de <see cref="String.Format(IFormatProvider,string,object[])"/>.</param>
        /// <returns>Une chaîne formattée.</returns>
        public static string UiFormat(this string format, params object[] args)
        {
            return string.Format(CultureInfo.CurrentUICulture, format, args);
        }
    }
}