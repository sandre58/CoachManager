using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum ExerciceType
    {
        [Display(Name = "Other", ResourceType = typeof(ExerciceTypeResources))]
        Other,

        [Display(Name = "WarmUp", ResourceType = typeof(ExerciceTypeResources))]
        WarmUp,

        [Display(Name = "Exercice", ResourceType = typeof(ExerciceTypeResources))]
        Exercice,

        [Display(Name = "Situation", ResourceType = typeof(ExerciceTypeResources))]
        Situation,

        [Display(Name = "Game", ResourceType = typeof(ExerciceTypeResources))]
        Game,

    }
}