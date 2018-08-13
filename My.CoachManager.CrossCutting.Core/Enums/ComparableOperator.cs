using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum ComparableOperator
    {
        [Display(Name = "EqualsTo", ResourceType = typeof(OperatorResources))]
        EqualsTo,

        [Display(Name = "NotEqualsTo", ResourceType = typeof(OperatorResources))]
        NotEqualsTo,

        [Display(Name = "LessThan", ResourceType = typeof(OperatorResources))]
        LessThan,

        [Display(Name = "GreaterThan", ResourceType = typeof(OperatorResources))]
        GreaterThan,

        [Display(Name = "LessEqualThan", ResourceType = typeof(OperatorResources))]
        LessEqualThan,

        [Display(Name = "GreaterEqualThan", ResourceType = typeof(OperatorResources))]
        GreaterEqualThan
    }
}