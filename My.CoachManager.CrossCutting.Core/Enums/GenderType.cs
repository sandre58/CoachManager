using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    /// <summary>
    /// Genders Types.
    /// </summary>
    public enum GenderType
    {
        [Display(Name = "Male", ResourceType = typeof(GenderTypeResources))]
        Male = 0,

        [Display(Name = "Female", ResourceType = typeof(GenderTypeResources))]
        Female = 1
    }
}
