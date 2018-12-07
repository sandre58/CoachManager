using System.ComponentModel.DataAnnotations;
using My.CoachManager.Presentation.Prism.Modules.Core.Resources;

namespace My.CoachManager.Presentation.Prism.Modules.Core.Enums
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