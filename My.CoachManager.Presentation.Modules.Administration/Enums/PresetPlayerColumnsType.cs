using My.CoachManager.Presentation.Modules.Administration.Resources;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Presentation.Modules.Administration.Enums
{
    public enum PresetPlayerColumnsType
    {
        [Display(Name = "General", ResourceType = typeof(PlayerResources))]
        GeneralInformations,

        [Display(Name = "Club", ResourceType = typeof(PlayerResources))]
        ClubInformations,

        [Display(Name = "Morphology", ResourceType = typeof(PlayerResources))]
        BodyInformations
    }
}