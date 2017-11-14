using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Resources;
using System.Windows.Data;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Core.Converters
{
    public class EnumToStringConverter : IValueConverter
    {
        #region Static Fields

        private static EnumToStringConverter _instance;

        #endregion Static Fields

        #region Public Properties

        /// <summary>
        /// Return a unique _instance of <see cref="EnumToStringConverter"/>.
        /// </summary>
        public static EnumToStringConverter Instance
        {
            get
            {
                return _instance ?? (_instance = new EnumToStringConverter());
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null)
            {
                return string.Empty;
            }

            var enumValue = value as Enum;

            if (parameter != null)
            {
                if (enumValue != null)
                {
                    var valueInfo = enumValue.GetType().GetMember(enumValue.ToString());
                    var valueAttribute = valueInfo[0].GetCustomAttributes(typeof(DisplayAttribute), false);
                    return new ResourceManager((Type)parameter).GetString(((DisplayAttribute)valueAttribute[0]).Name);
                }
            }

            return enumValue != null ? enumValue.ToDisplay() : string.Empty;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        #endregion Public Methods and Operators
    }
}