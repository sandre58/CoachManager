using My.CoachManager.Presentation.Modules.Roster.Resources;
using System.ComponentModel.DataAnnotations;

namespace My.CoachManager.Presentation.Modules.Roster.Enums
{
    public enum PresetRosterPlayersColumnsType
    {
        [Display(Name = "General", ResourceType = typeof(RosterPlayerResources))]
        GeneralInformations,

        [Display(Name = "Club", ResourceType = typeof(RosterPlayerResources))]
        ClubInformations,

        [Display(Name = "Morphology", ResourceType = typeof(RosterPlayerResources))]
        BodyInformations
    }
}