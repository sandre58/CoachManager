using System;
using System.Linq;
using System.Runtime.Serialization;

using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Core.Models.Filters
{
    /// <summary>
    /// Defines the logic for equality filter
    /// </summary>
    public class EnumValuesFilter : SelectedValuesFilter<Tuple<string, object>>
    {
        #region Constructors

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValueFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="isFixed"></param>
        public EnumValuesFilter(string propertyName, bool isFixed = false)
            : base(propertyName,isFixed)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EnumValueFilter"/> class.
        /// </summary>
        /// <param name="propertyName">The property info.</param>
        /// <param name="enumType"></param>
        /// <param name="isFixed"></param>
        public EnumValuesFilter(string propertyName, Type enumType, bool isFixed = false)
            : base(propertyName,isFixed)
        {
            var enumValues = Enum.GetValues(enumType);

            AllowedValues = (from Enum enumValue in enumValues
                             select
                                 new Tuple<string, object>(enumValue.ToDisplay(), enumValue)).ToList();
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected EnumValuesFilter()
        {
        }

        /// <inheritdoc />
        /// <summary>
        /// Constructor used for the serialization.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected EnumValuesFilter(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        #endregion Constructors
    }
}
