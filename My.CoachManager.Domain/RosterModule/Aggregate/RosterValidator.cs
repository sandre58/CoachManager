using FluentValidation;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.RosterModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class RosterValidator : AbstractValidator<Roster>
    {

        /// <summary>
        /// Initialise a new instance of <see cref="RosterValidator"/>.
        /// </summary>
        public RosterValidator()
        {
        }

    }
}
