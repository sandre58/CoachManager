using System.ComponentModel.DataAnnotations;
using My.CoachManager.Presentation.Prism.Modules.Administration.Resources;

namespace My.CoachManager.Presentation.Prism.Modules.Administration.Enums
{
    public enum PresetColumnsType
    {
        [Display(Name = "General", ResourceType = typeof(PlayerResources))]
        GeneralInformations,

        [Display(Name = "Club", ResourceType = typeof(PlayerResources))]
        ClubInformations,

        [Display(Name = "Morphology", ResourceType = typeof(PlayerResources))]
        BodyInformations
    }
}