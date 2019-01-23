using System;
using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class TrainingFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static TrainingDto Get(TrainingModel item, CrudStatus crudStatus)
        {
            if (item == null) return null;

            return new TrainingDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                EndDate = item.EndDate,
                IsCancelled = item.IsCancelled,
                Place = item.Place,
                StartDate = item.StartDate
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static TrainingModel Get(TrainingDto dto)
        {
            if (dto == null) return null;

            var result = new TrainingModel
            {
                Id = dto.Id,
                EndDate = dto.EndDate,
                IsCancelled = dto.IsCancelled,
                Place = dto.Place,
                StartDate = dto.StartDate,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
            result.ResetModified();

            return result;
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <returns>The model.</returns>
        public static TrainingModel Create(DateTime? startDate, DateTime? endDate, TimeSpan startTime, TimeSpan endTime, string place)
        {
            var result = new TrainingModel
            {
                EndDate = endDate ?? new DateTime(),
                Place = place,
                StartDate = startDate ?? new DateTime(),
                StartTime = startTime,
                EndTime = endTime
            };
            result.ResetModified();

            return result;
        }

        public static TrainingModel Empty => new TrainingModel();
    }
}