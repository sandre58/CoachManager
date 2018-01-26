using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    public enum LogicalOperator
    {
        [Display(Name = "And", ResourceType = typeof(FilterOperatorResources))]
        And,

        [Display(Name = "Or", ResourceType = typeof(FilterOperatorResources))]
        Or,
    }
}