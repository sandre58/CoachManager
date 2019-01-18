using System;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.RosterModule.Aggregate;

namespace My.CoachManager.Domain.TrainingModule.Aggregate
{
    /// <summary>
    /// The category factory.
    /// </summary>
    public static class TrainingFactory
    {
        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>The entity.</returns>
        public static Training CreateEntity(TrainingDto item)
        {
            if (item == null) return null;

            return new Training
            {
                Id = item.Id,
                RosterId = item.RosterId,
                EndDate = item.EndDate,
                IsCancelled = item.IsCancelled,
                Place = item.Place,
                StartDate = item.StartDate
            };
        }

        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <returns>The entity.</returns>
        public static Training CreateTraining(int rosterId, DateTime date, TimeSpan startTime, TimeSpan endTime, string place)
        {
            var startDate = new DateTime(date.Year, date.Month, date.Day, startTime.Hours, startTime.Minutes, startTime.Seconds);
            var endDate = new DateTime(date.Year, date.Month, date.Day, endTime.Hours, endTime.Minutes, endTime.Seconds);
            return new Training
            {
                RosterId = rosterId,
                StartDate = startDate,
                EndDate = endDate,
                IsCancelled = false,
                Place = place,
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(TrainingDto item, Training entity)
        {
            entity.Id = item.Id;
            entity.RosterId = item.RosterId;
            entity.EndDate = item.EndDate;
            entity.IsCancelled = item.IsCancelled;
            entity.StartDate = item.StartDate;
            entity.Place = item.Place;

            return true;
        }

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static TrainingDto Get(Training item)
        {
            if (item == null) return null;

            return new TrainingDto
            {
                Id = item.Id,
                RosterId = item.RosterId,
                Roster = RosterFactory.Get(item.Roster),
                EndDate = item.EndDate,
                IsCancelled = item.IsCancelled,
                Place = item.Place,
                StartDate = item.StartDate,
                CreatedDate = item.CreatedDate,
                CreatedBy = item.CreatedBy,
                ModifiedDate = item.ModifiedDate,
                ModifiedBy = item.ModifiedBy
            };
        }

    }
}