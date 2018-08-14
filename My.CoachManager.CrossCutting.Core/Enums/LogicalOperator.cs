using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Models.Filters
{
    public enum LogicalOperator
    {
        [Display(Name = "And", ResourceType = typeof(OperatorResources))]
        And,

        [Display(Name = "Or", ResourceType = typeof(OperatorResources))]
        Or,
    }
}