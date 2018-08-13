using My.CoachManager.Application.Dtos;

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

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="model"></param>
        /// <returns>The model.</returns>
        public static TContactDto GetContact<TContactDto>(ContactModel model) where TContactDto : ContactDto, new()
        {
            return new TContactDto()
            {
                Id = model.Id,
                Label = model.Label,
                Value = model.Value,
                Default = model.Default,
                PersonId = model.PersonId
            };
        }
    }
}