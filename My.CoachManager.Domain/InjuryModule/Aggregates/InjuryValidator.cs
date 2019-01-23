using FluentValidation;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.InjuryModule.Aggregates
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class InjuryValidator : AbstractValidator<Injury>
    {

        /// <summary>
        /// Initialise a new instance of <see cref="InjuryValidator"/>.
        /// </summary>
        public InjuryValidator()
        {

        }
    }
}
