using System.ComponentModel.DataAnnotations;

using My.CoachManager.CrossCutting.Resources.Enums;

namespace My.CoachManager.CrossCutting.Core.Enums
{
    public enum LogicalOperator
    {
        [Display(Name = "And", ResourceType = typeof(OperatorResources))]
        And,

        [Display(Name = "Or", ResourceType = typeof(OperatorResources))]
        Or,
    }
}
