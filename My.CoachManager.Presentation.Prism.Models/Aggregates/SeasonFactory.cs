﻿using My.CoachManager.Application.Dtos;
using My.CoachManager.Application.Dtos.Season;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The season factory.
    /// </summary>
    public static class SeasonFactory
    {
        /// <summary>
        /// Convert the model to DTO.
        /// </summary>
        /// <param name="model">The model.</param>
        /// <param name="crudStatus">The crud status.</param>
        /// <returns>The DTO from the model.</returns>
        public static SeasonDto Get(SeasonModel model, CrudStatus crudStatus)
        {
            return new SeasonDto
            {
                CrudStatus = crudStatus,
                Id = model.Id,
                Code = model.Code,
                Label = model.Label,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static SeasonModel Get(SeasonDto dto)
        {
            if (dto == null) return null;

            return new SeasonModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                StartDate = dto.StartDate,
                EndDate = dto.EndDate,
                Order = dto.Order,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}