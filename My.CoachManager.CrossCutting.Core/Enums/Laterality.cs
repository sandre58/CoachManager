using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// Genders Types.
    /// </summary>
    public enum Laterality
    {
        [Display(Name = "Unknown", ResourceType = typeof(LateralityResources))]
        Unknown,

        [Display(Name = "RightHander", ResourceType = typeof(LateralityResources))]
        RightHander,

        [Display(Name = "LeftHander", ResourceType = typeof(LateralityResources))]
        LeftHander
    }
}