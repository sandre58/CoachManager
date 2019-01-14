using FluentValidation.Results;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.TrainingModule.Aggregate;

namespace My.CoachManager.Domain.TrainingModule.Services
{
    public class TrainingDomainService : ITrainingDomainService
    {

        #region Methods
        /// <summary>
        /// Validates entity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public ValidationResult Validate(Training entity)
        {
            var validator = new TrainingValidator();
            return validator.Validate(entity);
        }

        #endregion Methods
    }
}