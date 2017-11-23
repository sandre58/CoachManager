using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// Ground for stadium.
    /// </summary>
    public enum Ground
    {
        [Display(Name = "Unknown", ResourceType = typeof(GroundResources))]
        Unknown = 0,

        [Display(Name = "Turf", ResourceType = typeof(GroundResources))]
        Turf = 1,

        [Display(Name = "Astroturf", ResourceType = typeof(GroundResources))]
        Astroturf = 1,

        [Display(Name = "Futsal", ResourceType = typeof(GroundResources))]
        Futsal = 1,

        [Display(Name = "Stab", ResourceType = typeof(GroundResources))]
        Stab = 1,

        [Display(Name = "Beach", ResourceType = typeof(GroundResources))]
        Beach = 1
    }
}