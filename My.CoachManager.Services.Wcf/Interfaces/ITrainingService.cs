using System;
using System.Collections.Generic;
using System.ServiceModel;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Services.Wcf.Interfaces
{
    /// <summary>
    /// Training Service Interface.
    /// </summary>
    [ServiceContract]
    public interface ITrainingService
    {
        /// <summary>
        /// Get all dtos list.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<TrainingDto> GetTrainings(int rosterId);

        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int SaveTraining(int rosterId, TrainingDto dto);

        /// <summary>
        /// Remove a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        void RemoveTraining(TrainingDto dto);

        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        TrainingDto GetTrainingById(int id);

        /// <summary>
        /// Add trainings between two date.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        IList<TrainingDto> AddTrainings(int rosterId, DateTime startDate, DateTime endDate, TimeSpan startTime,
            TimeSpan endTime, string place, IList<DayOfWeek> days);

        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        [OperationContract]
        int SaveTrainingAttendances(int trainingId, IList<TrainingAttendanceDto> attendances);

        /// <summary>
        /// Gets players for a specific training.
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        [OperationContract]
        IList<RosterPlayerDto> GetPlayersForTraining(int trainingId);
    }
}