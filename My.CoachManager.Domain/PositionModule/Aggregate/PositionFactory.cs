using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PositionModule.Aggregate
{

    /// <summary>
    /// The category factory.
    /// </summary>
    public static class PositionFactory
    {

        /// <summary>
        /// Convert the entity to DTO.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <returns>Result of the convert to DTO.</returns>
        public static PositionDto Get(Position item)
        {
            if (item == null) return null;

            return new PositionDto
            {
                Id = item.Id,
                Type = item.Type,
                Order = item.Order,
                Label = item.Label,
                Column = item.Column,
                Code = item.Code,
                Row = item.Row,
                Side = item.Side
            };
        }
    }
}
