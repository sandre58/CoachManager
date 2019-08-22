using System.ComponentModel.DataAnnotations;
using My.CoachManager.Presentation.Wpf.Modules.Admin.Resources;

namespace My.CoachManager.Presentation.Wpf.Modules.Admin.Enums
{
    public enum PresetPlayerColumnsType
    {
        [Display(Name = "General", ResourceType = typeof(PlayerResources))]
        GeneralInformation,

        [Display(Name = "Club", ResourceType = typeof(PlayerResources))]
        ClubInformation,

        [Display(Name = "Morphology", ResourceType = typeof(PlayerResources))]
        BodyInformation
    }
}
