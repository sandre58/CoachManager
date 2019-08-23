using FluentValidation;

using My.CoachManager.CrossCutting.Resources;
using My.CoachManager.Domain.Core;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.ReferenceModule.Aggregate;

namespace My.CoachManager.Domain.SeasonModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class SeasonValidator : ReferenceValidator<Season>
    {
        
        /// <summary>
        /// Initialise a new instance of <see cref="ReferenceValidator{TEntity}"/>.
        /// </summary>
        public SeasonValidator(IRepository<Season> repository) : base(repository)
        {
            RuleFor(x => x.StartDate).LessThan(x => x.EndDate)
                .WithMessage(ValidationMessageResources.StartDateLessThanEndDateMessage);
            RuleFor(x => x.EndDate).GreaterThan(x => x.StartDate)
                .WithMessage(ValidationMessageResources.StartDateLessThanEndDateMessage);
        }

    }
}
