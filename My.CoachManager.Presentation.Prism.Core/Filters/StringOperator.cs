using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    public enum StringOperator
    {
        [Display(Name = "Is", ResourceType = typeof(FilterOperatorResources))]
        Is,

        [Display(Name = "IsNot", ResourceType = typeof(FilterOperatorResources))]
        IsNot,

        [Display(Name = "StartsWith", ResourceType = typeof(FilterOperatorResources))]
        StartsWith,

        [Display(Name = "EndsWith", ResourceType = typeof(FilterOperatorResources))]
        EndsWith,

        [Display(Name = "Contains", ResourceType = typeof(FilterOperatorResources))]
        Contains
    }
}