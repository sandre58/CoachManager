using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum BinaryOperator
    {
        [Display(Name = "Is", ResourceType = typeof(OperatorResources))]
        Is,

        [Display(Name = "IsNot", ResourceType = typeof(OperatorResources))]
        IsNot,
    }
}
