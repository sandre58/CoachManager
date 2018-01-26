using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    public enum BinaryOperator
    {
        [Display(Name = "Is", ResourceType = typeof(FilterOperatorResources))]
        Is,

        [Display(Name = "IsNot", ResourceType = typeof(FilterOperatorResources))]
        IsNot,
    }
}