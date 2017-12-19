using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace My.CoachManager.CrossCutting.Core.Extensions
{
    public static class AttributeExtensions
    {
        /// <summary>
        ///     A generic extension method that aids in reflecting
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this Enum enumValue)
                where TAttribute : Attribute
        {
            return enumValue.GetType()
                            .GetMember(enumValue.ToString())
                            .First()
                            .GetCustomAttribute<TAttribute>();
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static TAttribute GetAttribute<TAttribute>(this PropertyInfo propertyInfo)
            where TAttribute : Attribute
        {
            var attr = propertyInfo
                .GetCustomAttribute<TAttribute>();

            if (attr == null)
            {
                if (propertyInfo.DeclaringType != null)
                {
                    var atts = propertyInfo.DeclaringType.GetCustomAttributes(
                        typeof(MetadataTypeAttribute), true);
                    if (atts.Length == 0)
                        return null;

                    var metaAttr = atts[0] as MetadataTypeAttribute;
                    if (metaAttr != null)
                    {
                        var metaProperty = metaAttr.MetadataClassType.GetProperty(propertyInfo.Name);
                        if (metaProperty == null)
                            return null;
                        return metaProperty.GetAttribute<TAttribute>();
                    }
                }
            }
            return attr;
        }

        /// <summary>
        ///     A generic extension method that aids in reflecting
        ///     and retrieving any attribute that is applied to an `Enum`.
        /// </summary>
        public static string GetDisplayName(this PropertyInfo propertyInfo)
        {
            var attr = GetAttribute<DisplayAttribute>(propertyInfo);
            return attr != null ? attr.GetName() : "";
        }
    }
}