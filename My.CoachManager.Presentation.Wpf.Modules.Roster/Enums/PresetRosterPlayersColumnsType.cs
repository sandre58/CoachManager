using System.ComponentModel.DataAnnotations;
using My.CoachManager.Presentation.Wpf.Modules.Roster.Resources;

namespace My.CoachManager.Presentation.Wpf.Modules.Roster.Enums
{
    public enum PresetRosterPlayersColumnsType
    {
        [Display(Name = "General", ResourceType = typeof(RosterPlayerResources))]
        GeneralInformation,

        [Display(Name = "Club", ResourceType = typeof(RosterPlayerResources))]
        ClubInformation,

        [Display(Name = "Morphology", ResourceType = typeof(RosterPlayerResources))]
        BodyInformation
    }
}
