using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Markup;
using My.CoachManager.CrossCutting.Core.Helpers;

namespace My.CoachManager.Presentation.Prism.Controls.Extensions
{
    public class EnumExtension : MarkupExtension
    {
        #region Fields

        private Type _enumType;

        private IEnumerable<object> _enumsToExclude;

        #endregion Fields

        #region Constructors and Destructors

        public EnumExtension()
        {
        }

        public EnumExtension(Type enumType)
        {
            if (enumType == null)
            {
                throw new ArgumentNullException("enumType");
            }

            EnumType = enumType;
        }

        public EnumExtension(Type enumType, object enumsToExclude)
            : this(enumType)
        {
            var enumsAsArray = enumsToExclude as Array;
            EnumsToExclude = enumsAsArray != null ? enumsAsArray.Cast<object>() : new[] { enumsToExclude };
        }

        #endregion Constructors and Destructors

        #region Public Properties

        public Type EnumType
        {
            get
            {
                return _enumType;
            }

            set
            {
                if (_enumType == value)
                {
                    return;
                }

                Type valueType = Nullable.GetUnderlyingType(value) ?? value;
                if (valueType.IsEnum == false)
                {
                    throw new ArgumentException("Type must be an Enum.");
                }

                _enumType = value;
            }
        }

        public IEnumerable<object> EnumsToExclude
        {
            get
            {
                return _enumsToExclude;
            }

            set
            {
                if (Equals(_enumsToExclude, value) || value == null)
                {
                    return;
                }

                Type invalidEnumType = value.Select(v => Nullable.GetUnderlyingType(v.GetType()) ?? v.GetType()).FirstOrDefault(e => e.IsEnum == false || e != EnumType);
                if (invalidEnumType != null)
                {
                    throw new ArgumentException(StringHelper.InvariantFormat("Wrong type : {0} instead of {1}", invalidEnumType.Name, EnumType.Name));
                }

                _enumsToExclude = value;
            }
        }

        #endregion Public Properties

        #region Public Methods and Operators

        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            var enumValues = Enum.GetValues(EnumType);

            return (from object enumValue in enumValues
                    where EnumsToExclude == null || !EnumsToExclude.Contains(enumValue)
                    select
                        new EnumerationMember
                        {
                            Value = enumValue,
                            ValueAsString = enumValue.ToString(),
                            Description = GetDescription(enumValue),
                            Display = GetDisplay(enumValue)
                        }).ToArray();
        }

        #endregion Public Methods and Operators

        #region Methods

        private string GetDescription(object enumValue)
        {
            var descriptionAttribute =
                EnumType.GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DescriptionAttribute), false)
                    .FirstOrDefault() as DescriptionAttribute;

            return descriptionAttribute != null ? descriptionAttribute.Description : enumValue.ToString();
        }

        private string GetDisplay(object enumValue)
        {
            var displayAttribute =
                EnumType.GetField(enumValue.ToString())
                    .GetCustomAttributes(typeof(DisplayAttribute), false)
                    .FirstOrDefault() as DisplayAttribute;

            return displayAttribute != null
                       ? new System.Resources.ResourceManager(displayAttribute.ResourceType).GetString(
                           displayAttribute.Name)
                       : enumValue.ToString();
        }

        #endregion Methods

        public class EnumerationMember
        {
            #region Public Properties

            public string Description { get; set; }

            public string Display { get; set; }

            public object Value { get; set; }

            public string ValueAsString { get; set; }

            #endregion Public Properties
        }
    }
}