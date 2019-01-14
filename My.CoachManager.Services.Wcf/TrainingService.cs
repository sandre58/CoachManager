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
        public int SaveTraining(TrainingDto dto)
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().SaveTraining(dto);
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
        public IList<TrainingDto> GetTrainings()
        {
            return ServiceLocator.Current.GetInstance<ITrainingAppService>().GetTrainings();
        }
    }
}