using System.ComponentModel.DataAnnotations;
using My.CoachManager.CrossCutting.Core.Resources.Enums;

namespace My.CoachManager.Presentation.Prism.Core.Filters
{
    public enum ComparableOperator
    {
        [Display(Name = "EqualsTo", ResourceType = typeof(FilterOperatorResources))]
        EqualsTo,

        [Display(Name = "NotEqualsTo", ResourceType = typeof(FilterOperatorResources))]
        NotEqualsTo,

        [Display(Name = "LessThan", ResourceType = typeof(FilterOperatorResources))]
        LessThan,

        [Display(Name = "GreaterThan", ResourceType = typeof(FilterOperatorResources))]
        GreaterThan,

        [Display(Name = "LessEqualThan", ResourceType = typeof(FilterOperatorResources))]
        LessEqualThan,

        [Display(Name = "GreaterEqualThan", ResourceType = typeof(FilterOperatorResources))]
        GreaterEqualThan,

        [Display(Name = "IsBetween", ResourceType = typeof(FilterOperatorResources))]
        IsBetween,

        [Display(Name = "IsNotBetween", ResourceType = typeof(FilterOperatorResources))]
        IsNotBetween,
    }
}