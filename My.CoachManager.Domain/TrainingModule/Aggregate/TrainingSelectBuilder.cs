using System;
using System.Linq.Expressions;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.TrainingModule.Aggregate
{
    public static class TrainingSelectBuilder
    {
        /// <summary>
        /// Creates the select builder.
        /// </summary>
        public static Expression<Func<Training, TrainingDto>> SelectTrainings()
        {
            return x => new TrainingDto
            {
                Id = x.Id,
                RosterId = x.RosterId,
                EndDate = x.EndDate,
                IsCancelled = x.IsCancelled,
                Place = x.Place,
                StartDate = x.StartDate
            };
        }
    }
}