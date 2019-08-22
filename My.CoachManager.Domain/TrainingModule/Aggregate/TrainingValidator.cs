using FluentValidation;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.TrainingModule.Aggregate
{

    /// <summary>
    /// Validates entity.
    /// </summary>
    public class TrainingValidator : AbstractValidator<Training>
    {

        /// <summary>
        /// Initialise a new instance of <see cref="TrainingValidator"/>.
        /// </summary>
        public TrainingValidator()
        {
        }

    }
}
