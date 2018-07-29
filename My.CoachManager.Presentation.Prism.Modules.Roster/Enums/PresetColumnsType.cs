﻿using System.ComponentModel.DataAnnotations;
using My.CoachManager.Presentation.Prism.Modules.Roster.Resources.Strings;

namespace My.CoachManager.Presentation.Prism.Modules.Roster.Enums
{
    public enum PresetColumnsType
    {
        [Display(Name = "GeneralInformations", ResourceType = typeof(PresetColumnsTypeResources))]
        GeneralInformations,

        [Display(Name = "ClubInformations", ResourceType = typeof(PresetColumnsTypeResources))]
        ClubInformations,

        [Display(Name = "BodyInformations", ResourceType = typeof(PresetColumnsTypeResources))]
        BodyInformations
    }
}