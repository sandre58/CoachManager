using System;
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
            var displayAttribute = value.GetType().GetField(value.ToString()).GetCustomAttributes(typeof(DisplayAttribute), false).FirstOrDefault() as DisplayAttribute;
            return
                (displayAttribute != null)
                    ? new ResourceManager(displayAttribute.ResourceType).GetString(displayAttribute.Name)
                    : value.ToString();
        }

        #endregion Public Methods and Operators
    }
}