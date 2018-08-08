using My.CoachManager.Application.Dtos.Person;

namespace My.CoachManager.Presentation.Prism.Models.Aggregates
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class ContactFactory
    {
        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="dto"></param>
        /// <returns>The model.</returns>
        public static TContactModel GetContact<TContactModel>(ContactDto dto) where TContactModel : ContactModel, new()
        {
            return new TContactModel()
            {
                Id = dto.Id,
                Label = dto.Label,
                Value = dto.Value,
                Default = dto.Default,
                PersonId = dto.PersonId,
                CreatedBy = dto.CreatedBy,
                CreatedDate = dto.CreatedDate,
                ModifiedBy = dto.ModifiedBy,
                ModifiedDate = dto.ModifiedDate
            };
        }
    }
}