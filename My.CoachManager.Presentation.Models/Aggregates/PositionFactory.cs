using My.CoachManager.Application.Dtos;

namespace My.CoachManager.Presentation.Models.Aggregates
{
    /// <summary>
    /// The position factory.
    /// </summary>
    public static class PositionFactory
    {
        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto">The dto.</param>
        /// <returns>The model.</returns>
        public static PositionModel Get(PositionDto dto)
        {
            if (dto == null) return null;

            var result = new PositionModel
            {
                Id = dto.Id,
                Code = dto.Code,
                Label = dto.Label,
                Description = dto.Description,
                Side = dto.Side,
                Type = dto.Type,
                Column = dto.Column,
                Row = dto.Row,
                Order = dto.Order
            };
            result.ResetModified();

            return result;
        }
    }
}
