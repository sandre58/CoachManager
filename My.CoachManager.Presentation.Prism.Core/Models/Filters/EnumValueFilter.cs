using System;
using System.Linq;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    public class EnumValueFilter : SelectedValueFilter<Enum, Tuple<string, object>>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValueFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        public EnumValueFilter(string propertyName)
            : base(propertyName)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValueFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="enumType"></param>
        public EnumValueFilter(string propertyName, Type enumType)
            : base(propertyName)
        {
            var enumValues = Enum.GetValues(enumType);

            AllowedValues = (from Enum enumValue in enumValues
                             select
                                 new Tuple<string, object>(enumValue.ToDisplay(), enumValue)).ToArray();
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected EnumValueFilter()
        {
        }

        #endregion Constructors
    }
}