﻿using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    /// <summary>
    /// The model factory.
    /// </summary>
    public static class ContactFactory
    {

        /// <summary>
        /// Create the entity from the DTO.
        /// </summary>
        /// <returns>The entity.</returns>
        public static TContact CreateEntity<TContact>(ContactDto contact, int personId) where TContact : Contact, new()
        {
            if (contact == null) return null;

            return new TContact
            {
                Id = contact.Id,
                Label = contact.Label,
                Value = contact.Value,
                Default = contact.Default,
                PersonId = personId
            };
        }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity<TContact, TContactDto>(TContactDto item, TContact entity) 
            where TContact : Contact
        where TContactDto : ContactDto
        {
            entity.Default = item.Default;
            entity.Label = item.Label;
            entity.Value = item.Value;

            return true;
        }

        /// <summary>
            /// Convert the DTO to model.
            /// </summary>
            /// <param name="contact"></param>
            /// <returns>The model.</returns>
            public static TContactDto GetContact<TContactDto>(Contact contact) where TContactDto : ContactDto, new()
        {
            if (contact == null) return null;

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
