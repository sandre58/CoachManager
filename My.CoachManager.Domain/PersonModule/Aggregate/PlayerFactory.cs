using System.Linq;
using My.CoachManager.Application.Dtos.Category;
using My.CoachManager.Application.Dtos.Persons;
using My.CoachManager.Domain.Entities;

namespace My.CoachManager.Domain.PersonModule.Aggregate
{
    public static class PlayerFactory
    {
        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public static PlayerDetailsDto CreatePlayerDetailsDto(Player player)
        {
            return new PlayerDetailsDto()
            {
                Id = player.Id,
                FirstName = player.FirstName,
                LastName = player.LastName,
                AddressId = player.AddressId,
                Address = player.Address != null ? player.Address.Row1 : string.Empty,
                PostalCode = player.Address != null ? player.Address.PostalCode : string.Empty,
                City = player.Address != null ? player.Address.City : string.Empty,
                Birthdate = player.Birthdate,
                Gender = player.Gender,
                LicenseNumber = player.LicenseNumber,
                Photo = player.Photo,
                PlaceOfBirth = player.PlaceOfBirth,
                Laterality = player.Laterality,
                CategoryId = player.CategoryId,
                CountryId = player.CountryId,
                Category = player.Category != null ? new CategoryDto()
                {
                    Id = player.Category.Id,
                    Label = player.Category.Label,
                    Order = player.Category.Order
                } : null,
                Country = player.Country != null ? new CountryDto()
                {
                    Id = player.Country.Id,
                    Label = player.Country.Label,
                    Code = player.Country.Code,
                    Flag = player.Country.Flag
                } : null,
                Emails = player.Contacts.OfType<Email>().Select(e => new EmailDto()
                {
                    Id = e.Id,
                    Label = e.Label,
                    Default = e.Default,
                    Value = e.Value,
                    PersonId = e.PersonId
                }).AsEnumerable(),
                Phones = player.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                {
                    Id = p.Id,
                    Label = p.Label,
                    Default = p.Default,
                    Value = p.Value,
                    PersonId = p.PersonId
                }).AsEnumerable(),
                Height = player.Height,
                Weight = player.Weight,
                ShoesSize = player.ShoesSize,
                Size = player.Size,
            };
        }

        /// <summary>
        /// Get a player.
        /// </summary>
        /// <returns></returns>
        public static PlayerDto CreatePlayerDto(Player player)
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
                Gender = player.Gender,
                LicenseNumber = player.LicenseNumber,
                Photo = player.Photo,
                PlaceOfBirth = player.PlaceOfBirth,
                Laterality = player.Laterality,
                CategoryId = player.CategoryId,
                CountryId = player.CountryId,
                Emails = player.Contacts.OfType<Email>().Select(e => new EmailDto()
                {
                    Id = e.Id,
                    Label = e.Label,
                    Default = e.Default,
                    Value = e.Value,
                    PersonId = e.PersonId
                }).AsEnumerable(),
                Phones = player.Contacts.OfType<Phone>().Select(p => new PhoneDto()
                {
                    Id = p.Id,
                    Label = p.Label,
                    Default = p.Default,
                    Value = p.Value,
                    PersonId = p.PersonId
                }).AsEnumerable(),
                Height = player.Height,
                Weight = player.Weight,
                ShoesSize = player.ShoesSize,
                Size = player.Size,
            };
        }
    }
}