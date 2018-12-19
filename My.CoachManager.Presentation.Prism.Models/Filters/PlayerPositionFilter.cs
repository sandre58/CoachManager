using System.Collections.Generic;
using My.CoachManager.CrossCutting.Core.Constants;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.Presentation.Prism.Core.Models.Filters;

namespace My.CoachManager.Presentation.Prism.Models.Filters
{
    /// <summary>
    ///
    /// </summary>
    public class PlayerPositionFilter : IntegerFilter
    {
        #region Members

        /// <summary>
        /// Gets or sets position Id.
        /// </summary>
        public int PositionId { get; set; }

        /// <summary>
        /// Gets the values to check for equality
        /// </summary>
        /// <value>The values.</value>
        public IEnumerable<PositionModel> AllowedValues { get; set; }

        #endregion

        #region Constructors

        public PlayerPositionFilter(string propertyName) : base(propertyName)
        {
            Minimum = 1;
            Maximum = PositionConstants.MaxRating;
        }

        public PlayerPositionFilter(string propertyName, ComplexComparableOperator comparaison, int from, int to, IEnumerable<PositionModel> allowedValues) : base(
            propertyName, comparaison, from, to)
        {
            AllowedValues = allowedValues;
        }

        /// <summary>
        /// Constructor used by serialization.
        /// </summary>
        protected PlayerPositionFilter()
        {
        }

        #endregion

        #region Methods

        /// <summary>
        /// Determines whether the specified target is a match.
        /// </summary>
        /// <param name="toCompare">The target.</param>
        /// <returns>
        /// 	<c>true</c> if the specified target is a match; otherwise, <c>false</c>.
        /// </returns>
        protected override bool IsMatchProperty(object toCompare)
        {
            if (!(toCompare is PlayerPositionModel playerPosition))
            {
                return false;
            }

            return playerPosition.PositionId == PositionId && base.IsMatchProperty(playerPosition.Rating);
        }

        #endregion
    }
}