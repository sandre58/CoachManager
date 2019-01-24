using System.Linq;
using My.CoachManager.Application.Dtos;
using My.CoachManager.Domain.AddressModule.Aggregate;
using My.CoachManager.Domain.CategoryModule.Aggregate;
using My.CoachManager.Domain.Entities;
using My.CoachManager.Domain.InjuryModule.Aggregates;
using My.CoachManager.Domain.PositionModule.Aggregate;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class PlayerFactory
    { 
            /// <summary>
            /// Create the entity from the DTO.
            /// </summary>
            /// <param name="item">The item.</param>
            /// <returns>The entity.</returns>
            public static Player CreateEntity(PlayerDto item)
            {
                if (item == null) return null;

                return new Player
                {
                    Id = item.Id,
                    FirstName = item.FirstName,
                    LastName = item.LastName,
                    Birthdate = item.Birthdate,
                    FromDate = item.FromDate,
                    Gender = item.Gender,
                    LicenseNumber = item.LicenseNumber,
                    Photo = item.Photo,
                    PlaceOfBirth = item.PlaceOfBirth,
                    Laterality = item.Laterality,
                    CategoryId = item.CategoryId,
                    CountryId = item.CountryId,
                    Height = item.Height,
                    Weight = item.Weight,
                    ShoesSize = item.ShoesSize,
                    Size = item.Size,
                    AddressId = item.AddressId,
                    Address = AddressFactory.CreateEntity(item.Address, item.PostalCode, item.City),
                    Contacts = item.Phones.Select(ContactFactory.CreateEntity<Phone>).Concat(item.Emails.Select(ContactFactory.CreateEntity<Email>).Cast<Contact>()).ToList(),
                    Positions = item.Positions.Select(CreatePosition).ToList()
                };
            }

        /// <summary>
        /// Updates the entity.
        /// </summary>
        /// <param name="item">The item.</param>
        /// <param name="entity">The entity.</param>
        public static bool UpdateEntity(PlayerDto item, Player entity)
        {
            entity.Id = item.Id;
            entity.FirstName = item.FirstName;
            entity.LastName = item.LastName;
            entity.AddressId = item.AddressId;
            entity.Birthdate = item.Birthdate;
            entity.FromDate = item.FromDate;
            entity.Gender = item.Gender;
            entity.LicenseNumber = item.LicenseNumber;
            entity.Photo = item.Photo;
            entity.PlaceOfBirth = item.PlaceOfBirth;
            entity.Laterality = item.Laterality;
            entity.CategoryId = item.CategoryId;
            entity.CountryId = item.CountryId;
            entity.Height = item.Height;
            entity.Weight = item.Weight;
            entity.ShoesSize = item.ShoesSize;
            entity.Size = item.Size;
            entity.Description = item.Description;
            entity.Contacts = item.Phones.Select(ContactFactory.CreateEntity<Phone>)
                .Concat(item.Emails.Select(ContactFactory.CreateEntity<Email>).Cast<Contact>()).ToList();
            entity.Positions = item.Positions.Select(CreatePosition).ToList();

            return true;
        }

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public static PlayerDto Get(Player player)
        {
            return new PlayerDto()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                AddressId = player.AddressId,
                Address = player.Address != null ? player.Address.Row1 : string.Empty,
                PostalCode = player.Address != null ? player.Address.PostalCode : string.Empty,
                City = player.Address != null ? player.Address.City : string.Empty,
                Birthdate = player.Birthdate,
                FromDate = player.FromDate,
                Gender = player.Gender,
                LicenseNumber = player.LicenseNumber,
                Photo = player.Photo,
                PlaceOfBirth = player.PlaceOfBirth,
                Laterality = player.Laterality,
                CategoryId = player.CategoryId,
                CountryId = player.CountryId,
                Category = CategoryFactory.Get(player.Category),
                Country = CountryFactory.Get(player.Country),
                Emails = player.Contacts.OfType<Email>().Select(ContactFactory.GetContact<EmailDto>),
                Phones = player.Contacts.OfType<Phone>().Select(ContactFactory.GetContact<PhoneDto>),
                Positions = player.Positions.Select(GetPosition).OrderBy(x => x.Position.Order),
                Injuries = player.Injuries.Select(InjuryFactory.Get).OrderBy(x => x.Date),
                Height = player.Height,
                Weight = player.Weight,
                ShoesSize = player.ShoesSize,
                Size = player.Size,
                CreatedDate = player.CreatedDate,
                CreatedBy = player.CreatedBy,
                ModifiedDate = player.ModifiedDate,
                ModifiedBy = player.ModifiedBy
            };
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <returns>The model.</returns>
        public static PlayerPosition CreatePosition(PlayerPositionDto position)
        {
            if (position == null) return null;

            var result = new PlayerPosition
            {
                Id = position.Id,
                PositionId = position.PositionId,
                PlayerId = position.PlayerId,
                IsNatural = position.IsNatural,
                Rating = position.Rating
            };

            return result;
        }

        /// <summary>
        /// Convert the DTO to model.
        /// </summary>
        /// <returns>The model.</returns>
        public static PlayerPositionDto GetPosition(PlayerPosition position)
        {
            if (position == null) return null;

            var result = new PlayerPositionDto
            {
                Id = position.Id,
                Position = PositionFactory.Get(position.Position),
                PositionId = position.PositionId,
                PlayerId = position.PlayerId,
                IsNatural = position.IsNatural,
                Rating = position.Rating
            };

            return result;
        }
    }
}