using System.Collections.Generic;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Application.Services.TrainingModule
{
    /// <summary>
    /// Interface defining the Training application services.
    /// </summary>
    public interface ITrainingAppService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        IList<TrainingDto> GetTrainings();

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveTraining(TrainingDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveTraining(TrainingDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        TrainingDto GetTrainingById(int id);
    }
}