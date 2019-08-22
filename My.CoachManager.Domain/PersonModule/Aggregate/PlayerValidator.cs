using FluentValidation;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class PlayerValidator : AbstractValidator<Player>
    {

        /// <summary>
        /// Initialise a new instance of <see cref="PlayerValidator"/>.
        /// </summary>
        public PlayerValidator()
        {

        }
    }
}
