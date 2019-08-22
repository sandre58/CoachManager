using System;
using System.Collections.ObjectModel;
using System.Linq;

using My.CoachManager.Application.Dtos;
using My.CoachManager.CrossCutting.Core.Enums;
using My.CoachManager.CrossCutting.Core.Extensions;

namespace My.CoachManager.Presentation.Models.Aggregates
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
        /// <param name="rosterId"></param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static TrainingDto Get(TrainingModel item, int rosterId, CrudStatus crudStatus)
        {
            if (item == null) return null;

            return new TrainingDto
            {
                CrudStatus = crudStatus,
                Id = item.Id,
                EndDate = item.EndDate,
                IsCancelled = item.IsCancelled,
                Place = item.Place,
                StartDate = item.StartDate,
                RosterId = rosterId
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
                RosterId = dto.RosterId,
                EndDate = dto.EndDate,
                IsCancelled = dto.IsCancelled,
                Place = dto.Place,
                Attendances = dto.Attendances != null ? dto.Attendances.Select(Get).ToObservableCollection() : new ObservableCollection<TrainingAttendanceModel>(),
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

        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="item">The model.</param>
        /// <returns>The DTO from the model.</returns>
        public static TrainingAttendanceDto Get(TrainingAttendanceModel item)
        {
            if (item == null) return null;

            return new TrainingAttendanceDto
            {
                Id = item.Id,
                RosterPlayerId = item.RosterPlayerId,
                Attendance = item.Attendance,
                Reason = item.Reason
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static TrainingAttendanceModel Get(TrainingAttendanceDto dto)
        {
            if (dto == null) return null;

            var result = new TrainingAttendanceModel
            {
                Id = dto.Id,
                Player = RosterFactory.Get(dto.RosterPlayer),
                RosterPlayerId = dto.RosterPlayerId,
                Attendance = dto.Attendance,
                Reason = dto.Reason,
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
        public static TrainingAttendanceModel CreateAttendance(RosterPlayerModel player, Attendance attendance)
        {
            var result = new TrainingAttendanceModel
            {
                Player = player,
                Attendance = attendance,
                RosterPlayerId = player.Id,
                Reason = string.Empty
            };
            result.ResetModified();

            return result;
        }
    }
}
