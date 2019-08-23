using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// Genders Types.
    /// </summary>
    public enum PositionSide
    {
        [Display(Name = "Left", ResourceType = typeof(PositionSideResources))]
        Left = 0,

        [Display(Name = "Center", ResourceType = typeof(PositionSideResources))]
        Center = 1,

        [Display(Name = "Right", ResourceType = typeof(PositionSideResources))]
        Right = 2
    }
}
