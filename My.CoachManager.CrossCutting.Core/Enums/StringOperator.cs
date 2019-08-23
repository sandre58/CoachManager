using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum StringOperator
    {
        [Display(Name = "Is", ResourceType = typeof(OperatorResources))]
        Is,

        [Display(Name = "IsNot", ResourceType = typeof(OperatorResources))]
        IsNot,

        [Display(Name = "StartsWith", ResourceType = typeof(OperatorResources))]
        StartsWith,

        [Display(Name = "EndsWith", ResourceType = typeof(OperatorResources))]
        EndsWith,

        [Display(Name = "Contains", ResourceType = typeof(OperatorResources))]
        Contains
    }
}
