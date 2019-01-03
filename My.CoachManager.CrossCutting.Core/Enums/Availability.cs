using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum Availability
    {
        [Display(Name = "Unavailable", ResourceType = typeof(AvailabilityResources))]
        Unavailable,

        [Display(Name = "Partial", ResourceType = typeof(AvailabilityResources))]
        Partial,

        [Display(Name = "Available", ResourceType = typeof(AvailabilityResources))]
        Available
    }
}