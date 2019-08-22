using FluentValidation.Results;

using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.TrainingModule.Services
{
    public interface ITrainingDomainService
    {
        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ValidationResult Validate(Training entity);
    }
}
