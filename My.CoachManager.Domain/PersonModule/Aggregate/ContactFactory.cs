using My.CoachManager.Application.Dtos.Person;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class ContactFactory
    {
   /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <param name="contact"></param>
        /// <returns>The model.</returns>
        public static TContactDto GetContact<TContactDto>(Contact contact) where TContactDto : ContactDto, new()
        {
            return new TContactDto()
            {
                Id = contact.Id,
                Label = contact.Label,
                Value = contact.Value,
                Default = contact.Default,
                PersonId = contact.PersonId
            };
        }
    }
}