using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Resources;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class EnumExtensions
    {
        #region Public Methods and Operators

        public static string ToDisplay(this Enum value)
        {
            return
                (value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() is DisplayAttribute displayAttribute)
                    ? new ResourceManager(displayAttribute.ResourceType).GetString(displayAttribute.Name)
                    : value.ToString();
        }

        public static string ToDescription(this Enum value)
        {
            return value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DescriptionAttribute), false).FirstOrDefault() is DescriptionAttribute descriptionAttribute ? descriptionAttribute.Description : value.ToString();
        }

        #endregion Public Methods and Operators
    }
}
