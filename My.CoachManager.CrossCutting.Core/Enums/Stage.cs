﻿using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum Stage
    {
        [Display(Name = "Other", ResourceType = typeof(StageResources))]
        Other,
        
    }
}
