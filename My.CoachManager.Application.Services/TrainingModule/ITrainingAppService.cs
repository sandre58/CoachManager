using System;
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
        IList<TrainingDto> GetTrainings(int rosterId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        int SaveTraining(int rosterId, TrainingDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        void RemoveTraining(int id);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        TrainingDto GetTrainingById(int id);

        /// <summary>
        /// Add trainings between two date.
        /// </summary>
        /// <returns></returns>
         IList<TrainingDto> AddTrainings(int rosterId, DateTime startDate, DateTime endDate, TimeSpan startTime,
            TimeSpan endTime, string place, IList<DayOfWeek> days);

        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        int SaveTrainingAttendances(int trainingId, IList<TrainingAttendanceDto> attendances);

        /// <summary>
        /// Gets players for a specific training.
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        IList<RosterPlayerDto> GetPlayersForTraining(int trainingId);
    }
}
