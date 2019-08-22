using FluentValidation;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.SquadModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class SquadValidator : AbstractValidator<Squad>
    {

        /// <summary>
        /// Initialise a new instance of <see cref="SquadValidator"/>.
        /// </summary>
        public SquadValidator()
        {
        }

    }
}
