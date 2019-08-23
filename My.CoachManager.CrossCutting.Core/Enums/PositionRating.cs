using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// PositionRating
    /// </summary>
    public enum PositionRating
    {
        [Display(Name = "Poor", ResourceType = typeof(PositionRatingResources))]
        Poor = 1,

        [Display(Name = "Average", ResourceType = typeof(PositionRatingResources))]
        Average = 2,

        [Display(Name = "Good", ResourceType = typeof(PositionRatingResources))]
        Good = 3,

        [Display(Name = "VeryGood", ResourceType = typeof(PositionRatingResources))]
        VeryGood = 4,

        [Display(Name = "Natural", ResourceType = typeof(PositionRatingResources))]
        Natural = 5,

    }
}
