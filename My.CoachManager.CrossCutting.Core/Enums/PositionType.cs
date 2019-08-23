using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// Genders Types.
    /// </summary>
    public enum PositionType
    {
        [Display(Name = "GoalKeeper", ResourceType = typeof(PositionTypeResources))]
        GoalKeeper = 0,

        [Display(Name = "Sweeper", ResourceType = typeof(PositionTypeResources))]
        Sweeper = 1,

        [Display(Name = "FullBack", ResourceType = typeof(PositionTypeResources))]
        FullBack = 2,

        [Display(Name = "CenterBack", ResourceType = typeof(PositionTypeResources))]
        CenterBack = 3,

        [Display(Name = "WingBack", ResourceType = typeof(PositionTypeResources))]
        WingBack = 4,

        [Display(Name = "DefensiveMidfielder", ResourceType = typeof(PositionTypeResources))]
        DefensiveMidfielder = 5,

        [Display(Name = "Midfielder", ResourceType = typeof(PositionTypeResources))]
        Midfielder = 6,

        [Display(Name = "AttackingMidfielder", ResourceType = typeof(PositionTypeResources))]
        AttackingMidfielder = 7,

        [Display(Name = "Winger", ResourceType = typeof(PositionTypeResources))]
        Winger = 8,

        [Display(Name = "Forward", ResourceType = typeof(PositionTypeResources))]
        Forward = 9,

        [Display(Name = "Striker", ResourceType = typeof(PositionTypeResources))]
        Striker = 10

    }
}
