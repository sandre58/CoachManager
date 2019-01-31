using System;
using System.Collections.Generic;
using CommonServiceLocator;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Services.TrainingModule;
using My.CoachManager.Services.Wcf.Interfaces;

namespace My.CoachManager.Services.Wcf
{
    /// <summary>
    /// Training Service.
    /// </summary>
    public class TrainingService : ITrainingService
    {
        /// <inheritdoc />
        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveTraining(int rosterId, TrainingDto dto)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().SaveTraining(rosterId,dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Create a dto.
        /// </summary>
        /// <returns></returns>
        public void RemoveTraining(TrainingDto dto)
        {
            ServiceLocator.Current.GetInstance<ITrainingAppService>().RemoveTraining(dto);
        }

        /// <inheritdoc />
        /// <summary>
        /// Gets a dto.
        /// </summary>
        /// <returns></returns>
        public TrainingDto GetTrainingById(int id)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().GetTrainingById(id);
        }

        /// <inheritdoc />
        /// <summary>
        /// Load all items.
        /// </summary>
        /// <returns></returns>
        public IList<TrainingDto> GetTrainings(int rosterId)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().GetTrainings(rosterId);
        }

        /// <summary>
        /// Add trainings between two date.
        /// </summary>
        /// <returns></returns>
        public IList<TrainingDto> AddTrainings(int rosterId, DateTime startDate, DateTime endDate, TimeSpan startTime,
            TimeSpan endTime, string place, IList<DayOfWeek> days)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().AddTrainings(rosterId, startDate, endDate, startTime,
            endTime, place, days);
        }

        /// <summary>
        /// Save a dto.
        /// </summary>
        /// <returns></returns>
        public int SaveTrainingAttendances(int trainingId, IList<TrainingAttendanceDto> attendances)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().SaveTrainingAttendances(trainingId, attendances);
        }

        /// <summary>
        /// Gets players for a specific training.
        /// </summary>
        /// <param name="trainingId"></param>
        /// <returns></returns>
        public IList<RosterPlayerDto> GetPlayersForTraining(int trainingId)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().GetPlayersForTraining(trainingId);
        }
    }
}