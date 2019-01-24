using My.CoachManager.CrossCutting.Core.Resources.Enums;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum InjurySeverity
    {
        [Display(Name = "Slight", ResourceType = typeof(InjurySeverityResources))]
        Slight,

        [Display(Name = "Minor", ResourceType = typeof(InjurySeverityResources))]
        Minor,

        [Display(Name = "Average", ResourceType = typeof(InjurySeverityResources))]
        Average,

        [Display(Name = "Serious", ResourceType = typeof(InjurySeverityResources))]
        Serious,
    }
}