using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// License State
    /// </summary>
    public enum LicenseState
    {
        [Display(Name = "Unknown", ResourceType = typeof(LicenseStateResources))]
        Unknown,

        [Display(Name = "Given", ResourceType = typeof(LicenseStateResources))]
        Given,

        [Display(Name = "Back", ResourceType = typeof(LicenseStateResources))]
        Back,

        [Display(Name = "Paid", ResourceType = typeof(LicenseStateResources))]
        Paid,
    }
}